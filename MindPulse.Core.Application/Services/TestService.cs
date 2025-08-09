using AutoMapper;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository, 
            IMapper mapper,
            ICategoryService categoryService,
            IQuestionnaireService questionnaireService)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _categoryService = categoryService;
            _questionnaireService = questionnaireService;
        }

        public async Task<ApiResponse<int>> CreateAsync(TestCreateDTO dto)
        {
            var entity = _mapper.Map<Test>(dto);
            await _testRepository.AddAsync(entity);
            return new ApiResponse<int>(200, data: entity.Id);
        }

        public async Task<ApiResponse<List<TestDTO>>> GetAllAsync()
        {
            var result = await _testRepository.GetAllAsync();
            var dtos = _mapper.Map<List<TestDTO>>(result);
            return new ApiResponse<List<TestDTO>>(200, dtos);
        }

        public async Task<ApiResponse<TestDTO>> GetByIdAsync(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
                return new ApiResponse<TestDTO>(404, "Test no encontrado");

            var dto = _mapper.Map<TestDTO>(test);
            return new ApiResponse<TestDTO>(200, dto);
        }

        public async Task<ApiResponse<List<TestDTO>>> GetAllByUserAsync(int userId)
        {
            var tests = await _testRepository.GetAllByUserAsync(userId);
            var dtoList = _mapper.Map<List<TestDTO>>(tests);
            return new ApiResponse<List<TestDTO>>(200, dtoList);
        }

        public async Task<ApiResponse<int>> SubmitTestAsync(TestResponseDTO input)
        {
            var testId = await _testRepository.SubmitTestAsync(input);
            return new ApiResponse<int>(200, data: testId);
        }
    }
}
