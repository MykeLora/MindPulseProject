using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Repositories.Recommendations
{
    public interface IRecommendationRepository : IGenericRepository<Recommendation>
    {
        Task<List<Recommendation>> GetByCategoryIdsAsync(List<int> categoryIds);

    }
}
