using AutoMapper;
using Azure;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Services
{
    public class UserResponseService : IUserResponseService
    {
        private readonly IUserResponseRepository _repository;
        private readonly IAiResponseService _aiResponseService;
        private readonly IOpenAiService _openAiService;
        private readonly IMapper _mapper;

        public UserResponseService(
            IUserResponseRepository repository,
            IAiResponseService aiResponseService,
            IOpenAiService openAiService,
            IMapper mapper)
        {
            _repository = repository;
            _aiResponseService = aiResponseService;
            _openAiService = openAiService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> CreateAsync(UserResponseCreateDTO dto)
        {
            var entity = _mapper.Map<UserResponse>(dto);
            await _repository.AddAsync(entity);

            // If it a free text response, process it with AI
            if (dto.TestResultId == null && dto.QuestionId == null && !string.IsNullOrWhiteSpace(dto.FreeResponse))
            {
                var aiResponse = await _openAiService.AnalyzeTextAsync(dto.FreeResponse);

                await _aiResponseService.CreateAsync(new AiResponseCreateDTO
                {
                    ChatResponse = aiResponse,
                    UserId = dto.UserId,
                });

                return new ApiResponse<string>(200, data: aiResponse);
            } else
            {
                // If it is a response to a test, we can return a default message
                return new ApiResponse<string>(200, data: "Respuesta a un test");
            }
        }

        public async Task<ApiResponse<List<UserResponsesDTO>>> GetByUserAsync(int userId)
        {
            var list = await _repository.GetByUserIdAsync(userId);
            return new ApiResponse<List<UserResponsesDTO>>(200, _mapper.Map<List<UserResponsesDTO>>(list));
        }

        public async Task<ApiResponse<List<UserResponsesDTO>>> GetFreeResponsesAsync(int userId)
        {
            var list = await _repository.GetFreeResponsesAsync(userId);
            return new ApiResponse<List<UserResponsesDTO>>(200, _mapper.Map<List<UserResponsesDTO>>(list));
        }
    }
}
