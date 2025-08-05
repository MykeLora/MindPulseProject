using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Repositories.Recommendations
{
    public interface IEducationalContentRepository
    {
        Task<EducationalContent> AddAsync(EducationalContent content);
        Task<List<EducationalContent>> GetAllAsync();
        Task<EducationalContent?> GetByIdAsync(int id);
        Task<List<EducationalContent>> GetByCategoryIdAsync(int categoryId);
    }
}
