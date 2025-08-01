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

namespace MindPulse.Infrastructure.Shared.Services
{
    public class TestService : ITestService
    {
        private readonly IGenericRepository<Test> _testRepository;
        private readonly IMapper _mapper;

        public TestService(IGenericRepository<Test> testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateAsync(TestCreateDTO dto)
        {
            var entity = _mapper.Map<Test>(dto);
            await _testRepository.AddAsync(entity);
            return new ApiResponse<int>(200, entity.Id);
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
            var tests = await _testRepository.FindAsync(t => t.UserId == userId);
            var dtoList = _mapper.Map<List<TestDTO>>(tests);
            return new ApiResponse<List<TestDTO>>(200, dtoList);
        }
    }
}
