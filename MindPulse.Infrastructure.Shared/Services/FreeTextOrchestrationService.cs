using AutoMapper;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces;
using MindPulse.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class FreeTextOrchestrationService : IFreeTextOrchestrationService
    {
        private readonly IUserResponseService _userResponseService;
        private readonly IAiResponseService _aiResponseService;
        private readonly IOpenAiService _openAiService;
        private readonly IEvaluationService _evaluationService;
        private readonly IRecommendationService _recommendationService;
        private readonly IMapper _mapper;

        public FreeTextOrchestrationService(
        IUserResponseService userResponseService,
        IAiResponseService aiResponseService,
        IOpenAiService openAiService,
        IEvaluationService evaluationService,
        IRecommendationService recommendationService,
        IMapper mapper)
        {
            _userResponseService = userResponseService;
            _aiResponseService = aiResponseService;
            _openAiService = openAiService;
            _evaluationService = evaluationService;
            _recommendationService = recommendationService;
            _mapper = mapper;
        }

        public async Task<EvaluationResult> AnalyzeAndStoreAsync(int userId, string input)
        {
            // Step 1: Saving the entry in UserResponse
            await _userResponseService.CreateAsync(new UserResponseCreateDTO
            {
                UserId = userId,
                FreeResponse = input
            });

            // Step 2: Getting last N messages (last 5 in this case)
            var allMessages = await _userResponseService.GetFreeResponsesAsync(userId);
            var recentMessages = allMessages.Data?.Select(m => m.FreeResponse)
                .Where(m => !string.IsNullOrEmpty(m))
                .TakeLast(5)
                .ToList() ?? new List<string>();

            var combined = string.Join("\n", recentMessages);

            // Step 3: Analyzing the text using OpenAI
            var resultText = await _openAiService.AnalyzeTextAsync($"Analiza este texto emocionalmente:\n{combined}");
            var parsed = new EvaluationResult
            {
                Category = "Detección Libre",
                Level = "Moderado", 
                Summary = resultText.Length > 200 ? resultText.Substring(0, 200) + "..." : resultText,
                Recommendation = "Recomendación de test." 
            };

            // Step 4: Saving the AI response
            await _aiResponseService.CreateAsync(new AiResponseCreateDTO
            {
                ChatResponse = resultText,
                UserId = userId
            });

            return parsed;
        }
    }  
}

