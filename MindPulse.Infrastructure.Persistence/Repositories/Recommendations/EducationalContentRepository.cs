using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class EducationalContentRepository : IEducationalContentRepository
    {
        private readonly ApplicationContext _context;

        public EducationalContentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<EducationalContent> AddAsync(EducationalContent content)
        {
            _context.EducationalContents.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<List<EducationalContent>> GetAllAsync()
        {
            return await _context.EducationalContents.ToListAsync();
        }

        public async Task<EducationalContent?> GetByIdAsync(int id)
        {
            return await _context.EducationalContents.FindAsync(id);
        }

        public async Task<List<EducationalContent>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.EducationalContents
                .Where(e => e.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
