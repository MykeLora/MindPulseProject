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

        public async Task<List<EducationalContent>> GetByCategoryIdsAsync(List<int> categoryIds)
        {
            return await _context.EducationalContents
                .Include(ec => ec.Category)
                .Where(ec => categoryIds.Contains(ec.CategoryId))
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
