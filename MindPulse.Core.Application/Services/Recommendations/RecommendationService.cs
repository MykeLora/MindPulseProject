using AutoMapper;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IMapper _mapper;

        public RecommendationService(IRecommendationRepository recommendationRepository, IMapper mapper)
        {
            _recommendationRepository = recommendationRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateAsync(RecommendationCreateDTO dto)
        {
            var entity = _mapper.Map<Recommendation>(dto);
            var created = await _recommendationRepository.AddAsync(entity);
            return new ApiResponse<int>(201, created.Id);
        }

        public async Task<ApiResponse<List<RecommendationDTO>>> GetAllAsync()
        {
            var contents = await _recommendationRepository.GetAllAsync();
            var dtos = _mapper.Map<List<RecommendationDTO>>(contents);
            return new ApiResponse<List<RecommendationDTO>>(200, dtos);
        }

        public async Task<ApiResponse<RecommendationDTO>> GetByIdAsync(int id)
        {
            var content = await _recommendationRepository.GetByIdAsync(id);
            if (content == null)
            {
                return new ApiResponse<RecommendationDTO>(404, "No se encontraron recomendaciones.");
            }

            var dto = _mapper.Map<RecommendationDTO>(content);
            return new ApiResponse<RecommendationDTO>(200, dto);
        }

        public async Task<ApiResponse<List<RecommendationDTO>>> GetAllByUserAsync(int userId)
        {
            var tests = await _recommendationRepository.GetAllByUserAsync(userId);
            var dtoList = _mapper.Map<List<RecommendationDTO>>(tests);
            return new ApiResponse<List<RecommendationDTO>>(200, dtoList);
        }

        public async Task<ApiResponse<List<RecommendationDTO>>> GetByCategoryIdAsync(int categoryId)
        {
            var list = await _recommendationRepository.GetByCategoryIdAsync(categoryId);
            if (list == null)
            {
                return new ApiResponse<List<RecommendationDTO>>(404, "No se encontraron recomendaciones para esta categoría");
            }

            var dtos = _mapper.Map<List<RecommendationDTO>>(list);
            return new ApiResponse<List<RecommendationDTO>>(200, dtos);
        }
    }
}
