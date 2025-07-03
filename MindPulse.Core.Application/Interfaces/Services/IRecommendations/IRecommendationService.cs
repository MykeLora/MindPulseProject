using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IRecommendationService : IGenericService<RecommendationDTO, RecommendationDTO, Recommendation, RecommendationDTO>
    {
        Task<ApiResponse<List<RecommendationDTO>>> GetByCategoryIdsAsync(List<int> categoryIds);
    }
}
