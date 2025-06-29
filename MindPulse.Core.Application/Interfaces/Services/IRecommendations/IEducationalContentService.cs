using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Services.Recommendations
{
    public interface IEducationalContentService : IGenericService<EducationalContentDTO, EducationalContentDTO, EducationalContent, EducationalContentDTO>
    {
        Task<ApiResponse<List<EducationalContentDTO>>> GetByCategoryIdAsync(int categoryId);

    }
}
