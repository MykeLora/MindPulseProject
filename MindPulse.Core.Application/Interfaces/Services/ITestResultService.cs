using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface ITestResultService
    {
        Task<ApiResponse<int>> CreateAsync(TestResultCreateDTO dto);
        Task<ApiResponse<List<TestResultDTO>>> GetAllAsync();
        Task<ApiResponse<TestResultDTO>> GetByIdAsync(int id);
        Task<ApiResponse<List<TestResultDTO>>> GetAllByUserAsync(int userId);

    }
}
