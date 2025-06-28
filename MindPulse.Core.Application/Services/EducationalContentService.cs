using AutoMapper;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class EducationalContentService : GenericService<EducationalContentDTO, EducationalContentDTO, EducationalContent, EducationalContentDTO>, IEducationalContentService
    {
        private readonly IEducationalContentRepository _educationalContentRepository;

        public EducationalContentService(IEducationalContentRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _educationalContentRepository = repo;
        }

        // Override GetAllAsync para incluir CategoryName en el DTO
        public override async Task<ApiResponse<List<EducationalContentDTO>>> GetAllAsync()
        {
            var contents = await _educationalContentRepository.GetAllWithCategoryAsync();

            var result = contents.Select(ec => new EducationalContentDTO
            {
                Id = ec.Id,
                Title = ec.Title,
                Url = ec.Url,
                CategoryId = ec.CategoryId,
                CategoryName = ec.Category?.Name
            }).ToList();

            return new ApiResponse<List<EducationalContentDTO>>(200, result);
        }

        public async Task<ApiResponse<List<EducationalContentDTO>>> GetByCategoryIdAsync(int categoryId)
        {
            var contents = await _educationalContentRepository.GetByCategoryIdAsync(categoryId);

            // Validación: si no hay contenidos asociados a la categoría
            if (contents == null || !contents.Any())
            {
                return new ApiResponse<List<EducationalContentDTO>>(404, $"No educational content found for category ID {categoryId}.");
            }

            var result = contents.Select(ec => new EducationalContentDTO
            {
                Id = ec.Id,
                Title = ec.Title,
                Url = ec.Url,
                CategoryId = ec.CategoryId,
                CategoryName = ec.Category?.Name
            }).ToList();

            return new ApiResponse<List<EducationalContentDTO>>(200, result);
        }
    }
}
