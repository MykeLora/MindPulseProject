using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Services.Recommendations
{
    public interface IEducationalContentService
    {
        Task<ApiResponse<int>> CreateAsync(EducationalContentCreateDTO educationalContentCreateDTO);
        Task<ApiResponse<List<EducationalContentDTO>>> GetAllAsync();
        Task<ApiResponse<EducationalContentDTO>> GetByIdAsync(int id);
        Task<ApiResponse<List<EducationalContentDTO>>> GetByCategoryIdAsync(int categoryId);
    }
}
