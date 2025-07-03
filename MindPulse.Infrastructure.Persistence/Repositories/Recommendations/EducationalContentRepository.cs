using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class EducationalContentRepository : GenericRepository<EducationalContent>, IEducationalContentRepository
    {
        private readonly ApplicationContext _context;

        public EducationalContentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EducationalContent>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.EducationalContents
                .Include(ec => ec.Category)
                .Where(ec => ec.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<EducationalContent>> GetAllWithCategoryAsync()
        {
            return await _context.EducationalContents
                .Include(ec => ec.Category)
                .ToListAsync();
        }
    }
}
