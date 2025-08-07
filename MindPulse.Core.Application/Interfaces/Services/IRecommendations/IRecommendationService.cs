using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IRecommendationService
    {
        Task<ApiResponse<int>> CreateAsync(RecommendationCreateDTO recommendationCreateDTO);
        Task<ApiResponse<List<RecommendationDTO>>> GetAllAsync();
        Task<ApiResponse<RecommendationDTO>> GetByIdAsync(int id);
        Task<ApiResponse<List<RecommendationDTO>>> GetAllByUserAsync(int userId);
        Task<ApiResponse<List<RecommendationDTO>>> GetByCategoryIdAsync(int categoryId);
    }
}
