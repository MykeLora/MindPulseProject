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

namespace MindPulse.Infrastructure.Persistence.Services
{
    public class UserResponseService : IUserResponseService
    {
        private readonly IUserResponseRepository _repository;
        private readonly IMapper _mapper;

        public UserResponseService(IUserResponseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserResponsesDTO>> CreateAsync(UserResponseCreateDTO dto)
        {
            var entity = _mapper.Map<UserResponse>(dto);
            var result = await _repository.AddAsync(entity);
            return new ApiResponse<UserResponsesDTO>(200, _mapper.Map<UserResponsesDTO>(result));
        }

        public async Task<ApiResponse<List<UserResponsesDTO>>> GetByUserAsync(int userId)
        {
            var list = await _repository.GetByUserIdAsync(userId);
            return new ApiResponse<List<UserResponsesDTO>>(200, _mapper.Map<List<UserResponsesDTO>>(list));
        }
    }
}
