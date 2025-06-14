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

    // TODO: Guardar resultado en base de datos cuando las entidades estén disponibles
    // await _evaluationRepository.AddAsync(new Evaluation { ... });

    public class EvaluationService : IEvaluationService
    {
        private readonly IOpenAiService _openAiService;

        public EvaluationService(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<EvaluationResult> EvaluateTestAsync(EvaluationRequest request)
        {
            var prompt = BuildPromptFromTest(request);

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            return ParseEvaluationResponse(rawResponse, "test");
        }

        public async Task<EvaluationResult> EvaluateFreeTextAsync(FreeTextEvaluationRequest request)
        {
            var combinedText = string.Join("\n", request.Messages);

            var prompt = $"Actúa como un psicólogo. Analiza el siguiente texto compuesto por varias entradas del usuario:\n\n{combinedText}\n\n" +
                          "Determina si hay signos de ansiedad, depresión u otro trastorno emocional. Devuelve un nivel de alerta (bajo, moderado, alto), un resumen breve y una recomendación.";

            var rawResponse = await _openAiService.AnalyzeTextAsync(prompt);

            return ParseEvaluationResponse(rawResponse, "libre");
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
