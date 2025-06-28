using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Infrastructure.Persistence.Context;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _context;  

        public CategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;  
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            // Normalize the input to lowercase and trimmed for better matching
            var normalized = name.Trim().ToLower();

            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == normalized);
        }
    }
}