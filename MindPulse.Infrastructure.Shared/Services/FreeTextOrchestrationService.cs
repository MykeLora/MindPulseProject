using AutoMapper;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
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

        public async Task<ApiResponse<List<ChatMessageDTO>>> GetFullChatAsync(int userId)
        {
            var userResponses = await _userResponseService.GetFreeResponsesAsync(userId);
            var aiResponses = await _aiResponseService.GetByUserIdAsync(userId);
            var allMessages = new List<ChatMessageDTO>();

            if (userResponses.Success && userResponses.Data != null)
            {
                allMessages.AddRange(userResponses.Data.Select(ur => new ChatMessageDTO
                {
                    Sender = "User",
                    Message = ur.FreeResponse,
                    Timestamp = ur.Created
                }));
            }

            if (aiResponses.Success && aiResponses.Data != null)
            {
                allMessages.AddRange(aiResponses.Data.Select(ai => new ChatMessageDTO
                {
                    Sender = "AI",
                    Message = ai.ChatResponse,
                    Timestamp = ai.Created
                }));
            }

            var ordered = allMessages.OrderBy(m => m.Timestamp)
                .ToList();

            return new ApiResponse<List<ChatMessageDTO>>(200, ordered);
        }

        public async Task<EvaluationResult> AnalyzeAndStoreAsync(int userId, string input)
        {
            // Step 1: Saving the User entry and the AI response
            await _userResponseService.CreateAsync(new UserResponseCreateDTO
            {
                UserId = userId,
                FreeResponse = input
            });

            // Step 2: Getting last N messages (last 5 in this case)
            var allMessages = await _userResponseService.GetFreeResponsesAsync(userId);
            var recentMessages = allMessages.Data?
                .Select(m => m.FreeResponse)
                .Where(m => !string.IsNullOrEmpty(m))
                .TakeLast(5)
                .ToList() ?? new List<string>();

            if (!recentMessages.Any())
                return new EvaluationResult
                {
                    Category = "Detección Libre",
                    Level = "Bajo",
                    Summary = "No se encontraron suficientes mensajes para realizar un análisis.",
                    Recommendation = "Continúa compartiendo cómo te sientes para brindarte una mejor evaluación."
                };

            var combined = string.Join("\n", recentMessages);

            // Step 3: Evaluating the combined messages 
            var evaluation = await _evaluationService.EvaluateFreeTextAsync(new FreeTextEvaluationRequest
            {
                UserId = userId,
                Messages = recentMessages
            });

            return evaluation;
        }
    }  
}

