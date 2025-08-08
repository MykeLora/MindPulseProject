using MindPulse.Core.Application.DTOs.Confidence;
using MindPulse.Core.Application.Wrappers;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IConfidenceService
    {
        Task<ApiResponse<ConfidenceDailyDTO>> GetDailyConfidenceByUserIdAsync(int userId);
        Task<ApiResponse<ConfidenceWeeklyDTO>> GetWeeklyConfidenceByUserIdAsync(int userId);
        Task<ApiResponse<ConfidenceMontlyDTO>> GetMonthlyConfidenceByUserIdAsync(int userId);
        Task<ApiResponse<ConfidenceGlobalDTO>> GetGlobalConfidenceByUserIdAsync(int userId);
    }
}