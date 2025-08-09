using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindPulse.Core.Application.DTOs.Evaluations.Analysis;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Orchestrations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services.Analysis
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IOpenAiService _openAiService;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly ApplicationContext _context;

        public EvaluationService(
            IOpenAiService openAiService, 
            ICategoryService categoryService,
            IQuestionnaireService questionnaireService,
            ApplicationContext context)
        {
            _openAiService = openAiService;
            _categoryService = categoryService;
            _questionnaireService = questionnaireService;
            _context = context;
        }

        ///// <summary>
        ///// Evalúa un test hecho por el usuario.
        ///// </summary>
        ///// <param name="request">Test hecho por el usuario.</param>
        ///// <returns>Resultado del análisis emocional.</returns>
        public async Task<TestAnalysisDTO> EvaluateTestAsync(TestResponseDTO input)
        {
            // Obtaining names for category and questionnaire
            var category = await _categoryService.GetByIdAsync(input.CategoryId);
            var questionnaire = await _questionnaireService.GetByIdAsync(input.QuestionnaireId);

            // Obtaining question and answer texts
            var submission = new TestSubmissionDTO
            {
                UserId = input.UserId,
                QuestionnaireName = questionnaire.Data.Title,
                CategoryName = category.Data.Name,
                Answers = new List<AnswerDetailDTO>()
            };

            foreach (var pair in input.Answers)
            {
                var question = await _context.Questions.FindAsync(pair.QuestionId);
                if (question == null)
                    throw new ArgumentException($"Pregunta con ID {pair.QuestionId} no encontrada.");

                var answerOption = await _context.AnswerOptions.FindAsync(pair.AnswerOptionId);
                if (answerOption == null)
                    throw new ArgumentException($"Opción de respuesta con ID {pair.AnswerOptionId} no encontrada.");

                submission.Answers.Add(new AnswerDetailDTO
                {
                    QuestionText = question.Text,
                    AnswerText = answerOption.Text
                });
            }

            var testResponses = new StringBuilder();

            foreach (var answer in submission.Answers)
            {
                testResponses.AppendLine($"Pregunta: {answer.QuestionText}");
                testResponses.AppendLine($"Respuesta: {answer.AnswerText}");
                testResponses.AppendLine();
            }

            var prompt = $"""
                        Actúa como un psicólogo clínico experto en salud mental.

                        El usuario ha completado el cuestionario: {submission.QuestionnaireName}
                        Categoría del test: {submission.CategoryName}

                        A continuación se presentan las respuestas del usuario:

                        {testResponses}

                        Analiza las respuestas y responde con los siguientes campos:

                        1. Un resumen del estado emocional del usuario (máximo 1000 caracteres).
                        2. Una recomendación breve para el usuario (máximo 500 caracteres).
                        3. Un nivel de confianza del 0% al 100% basado en las respuestas del usuario.
                        4. Si es posible, sugiere una fuente confiable en español sobre el tema. 
                        
                        Usa este formato exacto para las respuestas:

                        Resumen: ... (máximo 1000 caracteres)
                        Recomendación: ... (máximo 500 caracteres)
                        Confianza: ... (0% a 100%)
                        Nombre de la página: ...
                        Enlace: ...
                        Descripción: ... (máximo 1000 caracteres)

                        Si no existe una fuente confiable en español, escribe: "Nombre de la página: null"
                        """;

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            return ParseTestEvaluationResponse(rawResponse);

        }

        private TestAnalysisDTO ParseTestEvaluationResponse(string response)
        {
            var lines = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string summary = "", recommendation = "", pageName = "", url = "", description = "";
            float confidence = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("Resumen:", StringComparison.OrdinalIgnoreCase))
                    summary = line.Substring("Resumen:".Length).Trim();
                else if (line.StartsWith("Recomendación:", StringComparison.OrdinalIgnoreCase))
                    recommendation = line.Substring("Recomendación:".Length).Trim();
                else if (line.StartsWith("Confianza:", StringComparison.OrdinalIgnoreCase))
                {
                    var value = line.Substring("Confianza:".Length).Trim().Replace("%", "");
                    float.TryParse(value, out confidence);
                    confidence = confidence == 0 ? 60 : confidence;
                }
                else if (line.StartsWith("Nombre de la página:", StringComparison.OrdinalIgnoreCase))
                    pageName = line.Substring("Nombre de la página:".Length).Trim();
                else if (line.StartsWith("Enlace:", StringComparison.OrdinalIgnoreCase))
                    url = line.Substring("Enlace:".Length).Trim();
                else if (line.StartsWith("Descripción:", StringComparison.OrdinalIgnoreCase))
                    description = line.Substring("Descripción:".Length).Trim();
            }

            return new TestAnalysisDTO
            {
                Summary = summary[..Math.Min(1000, summary.Length)], // Limitar a 1000 caracteres
                Recommendation = recommendation[..Math.Min(500, recommendation.Length)], // Limitar a 500 caracteres
                Confidence = confidence / 100f, // Se convierte a 0.0 – 1.0
                Resource = pageName == "null" ? null : new EducationalContentSnippetDTO
                {
                    Title = pageName,
                    Url = url,
                    Description = description[..Math.Min(1000, description.Length)], // Limitar a 1000 caracteres
                }
            };
        }



        ///// <summary>
        ///// Evalúa un entradas libres del usuario.
        ///// </summary>
        ///// <param name="request">Mensajes enviados por el usuario.</param>
        ///// <returns>Resultado del análisis emocional.</returns>
        public async Task<FreeTextAnalysisDTO> EvaluateFreeTextAsync(FreeTextEvaluationRequest request)
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
                        3. Una puntuación de confianza del 0 al 100% que represente cuán confiado está el usuario según tu evaluación.

                        Formato de respuesta:
                        Nivel: ...
                        Resumen: ...
                        Confianza: ...
                        """;

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            var parsed = ParseFreeTextEvaluationResponse(rawResponse, "libre");

            // Storing the emotional analysis in the database
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

        private FreeTextAnalysisDTO ParseFreeTextEvaluationResponse(string response, string category)
        {
            var lines = response.Split('\n');
            var level = lines.FirstOrDefault(l => l.ToLower().Contains("nivel"))?.Split(':').Last().Trim();
            var summary = lines.FirstOrDefault(l => l.ToLower().Contains("resumen"))?.Split(':').Last().Trim();
            var confidenceStr = lines.FirstOrDefault(l => l.ToLower().Contains("confianza"))?.Split(':').Last().Trim();

            float.TryParse(confidenceStr?.Replace("%", "").Trim(), out float confidence);
            if (confidence == 0) confidence = 60f; // fallback

            return new FreeTextAnalysisDTO
            {
                Category = category,
                Level = level ?? "Bajo",
                Summary = summary ?? "",
                Confidence = confidence / 100f // Se convierte a 0.0 – 1.0
            };
        }

    }
}
