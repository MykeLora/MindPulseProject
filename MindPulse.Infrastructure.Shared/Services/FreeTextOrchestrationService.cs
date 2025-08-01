using AutoMapper;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Orchestrations;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
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
        private readonly ApplicationContext _context;

        public FreeTextOrchestrationService(
        IUserResponseService userResponseService,
        IAiResponseService aiResponseService,
        IOpenAiService openAiService,
        IEvaluationService evaluationService,
        IRecommendationService recommendationService,
        IMapper mapper,
        ApplicationContext context)
        {
            _userResponseService = userResponseService;
            _aiResponseService = aiResponseService;
            _openAiService = openAiService;
            _evaluationService = evaluationService;
            _recommendationService = recommendationService;
            _mapper = mapper;
            _context = context;
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

        public async Task<string> AnalyzeAndStoreAsync(int userId, string input)
        {
            // Step 1: Saving the User entry and the AI response
            var aiResponse = await _userResponseService.CreateAsync(new UserResponseCreateDTO
            {
                UserId = userId,
                FreeResponse = input
            });

            // Step 2: Verifying if the user has already sent 20 new messages. 
            var userResponses = await _userResponseService.GetFreeResponsesAsync(userId);
            var count = userResponses.Data?.Count ?? 0;

            if (count % 20 == 0) 
            {
                // Step 2.1: If the user has sent 20 new messages,
                // we get the last 20 messages to the AI and send them for evaluation.
                var recentMessages = userResponses.Data?
                .Select(m => m.FreeResponse)
                .Where(m => !string.IsNullOrEmpty(m))
                .TakeLast(20)
                .ToList() ?? new List<string>();

                if (recentMessages.Any())
                {
                    await _evaluationService.EvaluateFreeTextAsync(new FreeTextEvaluationRequest
                    {
                        UserId = userId,
                        Messages = recentMessages
                    });
                }
            }

            // Step 3: Returning the AI response
            return aiResponse.Data;
        }
    }  
}

