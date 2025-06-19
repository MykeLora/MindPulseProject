using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

      
    }
}
