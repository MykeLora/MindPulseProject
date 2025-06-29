using AutoMapper;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class RecommendationService : GenericService<RecommendationDTO, RecommendationDTO, Recommendation, RecommendationDTO>, IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository, IMapper mapper)
            : base(recommendationRepository, mapper)
        {
            _recommendationRepository = recommendationRepository;
        }

        public async Task<ApiResponse<List<RecommendationDTO>>> GetByCategoryIdsAsync(List<int> categoryIds)
        {
            var recommendations = await _recommendationRepository.GetByCategoryIdsAsync(categoryIds);

            var result = recommendations.Select(r => new RecommendationDTO
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                CategoryId = r.CategoryId,
                CategoryName = r.Category?.Name
            }).ToList();

            return new ApiResponse<List<RecommendationDTO>>(200, result);
        }

    }
}
