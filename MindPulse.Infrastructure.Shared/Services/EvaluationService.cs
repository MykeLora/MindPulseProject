using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IOpenAiService _openAiService;
        private readonly ApplicationContext _context;

        public EvaluationService(IOpenAiService openAiService, ApplicationContext context)
        {
            _openAiService = openAiService;
            _context = context;
        }

        ///// <summary>
        ///// Evalúa un test hecho por el usuario.
        ///// </summary>
        ///// <param name="request">Test hecho por el usuario.</param>
        ///// <returns>Resultado del análisis emocional.</returns>
        public async Task<EvaluationResult> EvaluateTestAsync(EvaluationRequest request)
        {
            var prompt = BuildPromptFromTest(request);

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            var parsed = ParseEvaluationResponse(rawResponse, "test");

            // Se guardan los resultados del test en la base de datos
            var testResult = new TestResult
            {
                CompletionDate = DateTime.UtcNow,
                Summary = parsed.Summary,
                SeverityScore = parsed.Level switch
                {
                    "Bajo" => 0.9f,
                    "Moderado" => 0.6f,
                    "Alto" => 0.3f,
                    _ => 0.5f
                },
                QuestionnaireId = request.CategoryId, // a modificarse cuando se conecte con cuestionarios
                UserId = request.UserId
            };

            await _context.TestResults.AddAsync(testResult);
            await _context.SaveChangesAsync();

            // Se guarda la recomendación en la base de datos
            var recommendation = new Recommendation
            {
                Title = $"Recomendación para resultado {parsed.Level}",
                Content = parsed.Recommendation,
                UrlSource = null, // A modificar cuando se conecte con una fuente de recomendación
                UserId = request.UserId,
                CategoryId = request.CategoryId
            };

            await _context.Recommendations.AddAsync(recommendation);
            await _context.SaveChangesAsync();

            return parsed;
        }

        private string BuildPromptFromTest(EvaluationRequest request)
        {
            var formatted = string.Join("\n", request.Answers.Select((qa, i) => $"{i + 1}. {qa.Question}: {qa.Answer}"));

            return $"Evalúa este test emocional centrado en la categoría ID {request.CategoryId}:\n{formatted}\n\n" +
                    "Responde con nivel de alerta, resumen breve y recomendación para el usuario.";
        }


        ///// <summary>
        ///// Evalúa un entradas libres del usuario.
        ///// </summary>
        ///// <param name="request">Mensajes enviados por el usuario.</param>
        ///// <returns>Resultado del análisis emocional.</returns>
        public async Task<EvaluationResult> EvaluateFreeTextAsync(FreeTextEvaluationRequest request)
        {
            var combinedText = string.Join("\n", request.Messages);
            var lastMessage = request.Messages.LastOrDefault() ?? "";

            var prompt = $"""
                        Actúa como un psicólogo experto en análisis emocional.

                        Analiza el siguiente texto compuesto por varias entradas del usuario:

                        {combinedText}

                        Identifica si hay signos de ansiedad, depresión u otro trastorno emocional. Devuelve lo siguiente:

                        1. Nivel de alerta (Bajo, Moderado, Alto).
                        2. Un resumen breve de tu análisis.
                        3. Una puntuación de confianza del 0 al 100% que represente cuán seguro estás de tu evaluación.

                        Formato de respuesta:
                        Nivel: ...
                        Resumen: ...
                        Confianza: ...
                        """;

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            var parsed = ParseEvaluationResponse(rawResponse, "libre");

            // Se guarda el resultado en la base de datos
            var entity = new EmotionalAnalysis
            {
                InputText = lastMessage,
                DetectedEmotion = parsed.Level,
                Confidence = parsed.Confidence,
                AnalysisDate = DateTime.UtcNow,
                UserId = request.UserId,
                Summary = parsed.Summary
            };

            await _context.EmotionAnalyses.AddAsync(entity);
            await _context.SaveChangesAsync();

            return parsed;
        }

        private EvaluationResult ParseEvaluationResponse(string response, string category)
        {
            var lines = response.Split('\n');
            var level = lines.FirstOrDefault(l => l.ToLower().Contains("nivel"))?.Split(':').Last().Trim();
            var summary = lines.FirstOrDefault(l => l.ToLower().Contains("resumen"))?.Split(':').Last().Trim();
            var recommendation = lines.FirstOrDefault(l => l.ToLower().Contains("recomendación"))?.Split(':').Last().Trim();
            var confidenceStr = lines.FirstOrDefault(l => l.ToLower().Contains("confianza"))?.Split(':').Last().Trim();

            float.TryParse(confidenceStr?.Replace("%", "").Trim(), out float confidence);
            if (confidence == 0) confidence = 60f; // fallback

            return new EvaluationResult
            {
                Category = category,
                Level = level ?? "Bajo",
                Summary = summary ?? "",
                Confidence = confidence / 100f // convertir a 0.0–1.0
            };
        }

    }
}
