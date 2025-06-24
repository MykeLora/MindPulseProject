using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class RecommendationRepository : GenericRepository<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
