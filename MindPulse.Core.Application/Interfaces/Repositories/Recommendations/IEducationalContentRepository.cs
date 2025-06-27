using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Repositories.Recommendations
{
    public interface IEducationalContentRepository : IGenericRepository<EducationalContent>
    {
       Task<List<EducationalContent>> GetByCategoryIdsAsync(List<int> categoryIds);

    }
}
