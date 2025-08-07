using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Interfaces.Repositories.Recommendations
{
    public interface IRecommendationRepository 
    {
        Task<Recommendation> AddAsync(Recommendation recommendation);
        Task<List<Recommendation>> GetAllAsync();
        Task<Recommendation?> GetByIdAsync(int id);
        Task<List<Recommendation>> GetAllByUserAsync(int userId);
        Task<List<Recommendation>> GetByCategoryIdAsync(int categoryId);
    }
}
