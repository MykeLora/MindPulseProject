using AutoMapper;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class EducationalContentService : IEducationalContentService
    {
        private readonly IEducationalContentRepository _educationalContentRepository;
        private readonly IMapper _mapper;

        public EducationalContentService(IEducationalContentRepository educationalContentRepository, IMapper mapper)
        {
            _educationalContentRepository = educationalContentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateAsync(EducationalContentCreateDTO dto)
        {
            var entity = _mapper.Map<EducationalContent>(dto);
            var created = await _educationalContentRepository.AddAsync(entity);
            return new ApiResponse<int>(201, created.Id);
        }

        public async Task<ApiResponse<List<EducationalContentDTO>>> GetAllAsync()
        {
            var contents = await _educationalContentRepository.GetAllAsync();
            var dtos = _mapper.Map<List<EducationalContentDTO>>(contents);
            return new ApiResponse<List<EducationalContentDTO>>(200, dtos);
        }

        public async Task<ApiResponse<EducationalContentDTO>> GetByIdAsync(int id)
        {
            var content = await _educationalContentRepository.GetByIdAsync(id);
            if (content == null) 
            {
                return new ApiResponse<EducationalContentDTO>(404, "Contenido no encontrado");
            }

            var dto = _mapper.Map<EducationalContentDTO>(content);
            return new ApiResponse<EducationalContentDTO>(200, dto);
        }

        public async Task<ApiResponse<List<EducationalContentDTO>>> GetByCategoryIdAsync(int categoryId)
        {
            var list = await _educationalContentRepository.GetByCategoryIdAsync(categoryId);
            if (list == null)
            {
                return new ApiResponse<List<EducationalContentDTO>>(404, "No se encontraron contenidos para esta categoría");
            }
            
            var dtos = _mapper.Map<List<EducationalContentDTO>>(list);
            return new ApiResponse<List<EducationalContentDTO>>(200, dtos);
        }

        
    }
}
