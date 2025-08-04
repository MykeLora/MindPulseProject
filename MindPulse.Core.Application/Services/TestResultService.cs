using AutoMapper;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services.IRecommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly IMapper _mapper;

        public TestResultService(ITestResultRepository testResultRepository, IMapper mapper)
        {
            _testResultRepository = testResultRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateAsync(TestResultCreateDTO dto)
        {
            var entity = _mapper.Map<TestResult>(dto);
            await _testResultRepository.AddAsync(entity);
            return new ApiResponse<int>(200, entity.Id);
        }

        public async Task<ApiResponse<TestResultDTO>> GetByIdAsync(int id)
        {
            var result = await _testResultRepository.GetByIdAsync(id);
            if (result == null)
                return new ApiResponse<TestResultDTO>(404, "Resultado no encontrado");

            var dto = _mapper.Map<TestResultDTO>(result);
            return new ApiResponse<TestResultDTO>(200, dto);
        }

        public async Task<ApiResponse<List<TestResultDTO>>> GetAllByUserAsync(int userId)
        {
            var results = await _testResultRepository.GetAllByUserAsync(userId);
            var dtoList = _mapper.Map<List<TestResultDTO>>(results);
            return new ApiResponse<List<TestResultDTO>>(200, dtoList);
        }
    }
}
