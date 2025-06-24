using AutoMapper;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class RecommendationService : GenericService<RecommendationDTO, RecommendationDTO, Recommendation, RecommendationDTO>, IRecommendationService
    {
        public RecommendationService(IRecommendationRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
