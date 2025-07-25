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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public RecommendationService(
            IRecommendationRepository recommendationRepository,
            ICategoryService categoryService,
            IMapper mapper
        ) : base(recommendationRepository, mapper)
        {
            _recommendationRepository = recommendationRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RecommendationDTO>> CreateAsync(RecommendationDTO dto)
        {

            var category = await _categoryService.GetByIdAsync(dto.CategoryId);
            if (category == null)
                return new ApiResponse<RecommendationDTO>(400, "La categoría especificada no existe.");

            var entity = _mapper.Map<Recommendation>(dto);

            var created = await _recommendationRepository.AddAsync(entity);

            var result = _mapper.Map<RecommendationDTO>(created);

            return new ApiResponse<RecommendationDTO>(201, result);
        }

        public async Task<ApiResponse<List<RecommendationDTO>>> GetByCategoryIdAsync(int categoryId)
        {
            var recommendations = await _recommendationRepository.GetByCategoryIdAsync(categoryId);

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
