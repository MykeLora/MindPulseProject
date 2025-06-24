using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class EducationalContentRepository : GenericRepository<EducationalContent>, IEducationalContentRepository
    {
        public EducationalContentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
