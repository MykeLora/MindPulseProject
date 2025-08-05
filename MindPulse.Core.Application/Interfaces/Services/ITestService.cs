using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface ITestService
    {
        Task<ApiResponse<int>> CreateAsync(TestCreateDTO dto);
        Task<ApiResponse<List<TestDTO>>> GetAllAsync();
        Task<ApiResponse<TestDTO>> GetByIdAsync(int id);
        Task<ApiResponse<List<TestDTO>>> GetAllByUserAsync(int userId);
        Task<ApiResponse<int>> SubmitTestAsync(TestResponseDTO input);
    }
}
