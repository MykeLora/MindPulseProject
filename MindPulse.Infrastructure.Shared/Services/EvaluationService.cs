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

    // TODO: Guardar resultado en EvaluationRequest y Recommendation (Se necesita conexion a cuestionarios aún)
    // await _evaluationRepository.AddAsync(new Evaluation { ... });

    public class EvaluationService : IEvaluationService
    {
        private readonly IOpenAiService _openAiService;
        private readonly ApplicationContext _context;

        public EvaluationService(IOpenAiService openAiService, ApplicationContext context)
        {
            _openAiService = openAiService;
            _context = context;
        }

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

        public async Task<EvaluationResult> EvaluateFreeTextAsync(FreeTextEvaluationRequest request)
        {
            var combinedText = string.Join("\n", request.Messages);

            var prompt = $"Actúa como un psicólogo. Analiza el siguiente texto compuesto por varias entradas del usuario:\n\n{combinedText}\n\n" +
                          "Determina si hay signos de ansiedad, depresión u otro trastorno emocional. Devuelve un nivel de alerta (bajo, moderado, alto), un resumen breve y una recomendación.";

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            var parsed = ParseEvaluationResponse(rawResponse, "libre");

            // Se guarda el resultado en la base de datos
            var entity = new EmotionalAnalysis
            {
                InputText = combinedText,
                DetectedEmotion = parsed.Level,
                Confidence = 0.85f, // Simulación de confianza
                AnalysisDate = DateTime.UtcNow,
                UserId = request.UserId
            };

            await _context.EmotionAnalyses.AddAsync(entity);
            await _context.SaveChangesAsync();

            return parsed;
        }

        private string BuildPromptFromTest(EvaluationRequest request)
        {
            var formatted = string.Join("\n", request.Answers.Select((qa, i) => $"{i + 1}. {qa.Question}: {qa.Answer}"));

            return $"Evalúa este test emocional centrado en la categoría ID {request.CategoryId}:\n{formatted}\n\n" +
                    "Responde con nivel de alerta, resumen breve y recomendación para el usuario.";
        }

        private EvaluationResult ParseEvaluationResponse(string raw, string mode)
        {
            return new EvaluationResult
            {
                Category = mode == "test" ? "Basado en categoría seleccionada" : "Detección Libre",
                Level = "Moderado",
                Summary = raw.Length > 150 ? raw.Substring(0, 150) + "..." : raw,
                Recommendation = "Refuerza tu rutina de descanso y considera hablar con un profesional si los síntomas persisten."
            };
        }

    }
}
