using AutoMapper;
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

namespace MindPulse.Core.Application.Services
{
    public class AiResponseService : IAiResponseService
    {
        private readonly IAiResponseRepository _repository;
        private readonly IMapper _mapper;

        public AiResponseService(IAiResponseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AiResponseDTO>> CreateAsync(AiResponseCreateDTO aiResponseCreateDTO)
        {
            var entity = _mapper.Map<AiResponse>(aiResponseCreateDTO);
            await _repository.AddAsync(entity);
            
            return new ApiResponse<AiResponseDTO>(200, _mapper.Map<AiResponseDTO>(entity));
        }

        public async Task<ApiResponse<List<AiResponseDTO>>> GetByUserIdAsync(int userId)
        {
            var responses = await _repository.GetByUserIdAsync(userId);
            var sorted = responses.OrderBy(r => r.Created).ToList();
            var dtoList = _mapper.Map<List<AiResponseDTO>>(sorted);

            return new ApiResponse<List<AiResponseDTO>>(200, dtoList);
        }
    }
}
