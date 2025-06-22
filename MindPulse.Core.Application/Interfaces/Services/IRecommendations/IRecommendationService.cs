using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IRecommendationService : IGenericService<RecommendationDTO, RecommendationDTO, Recommendation, RecommendationDTO>
    {
    }
}
