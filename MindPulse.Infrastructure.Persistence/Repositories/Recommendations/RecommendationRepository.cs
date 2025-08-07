using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationContext _context;  

        public RecommendationRepository(ApplicationContext context)
        {
            _context = context; 
        }

        public async Task<Recommendation> AddAsync(Recommendation content)
        {
            _context.Recommendations.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<List<Recommendation>> GetAllAsync()
        {
            return await _context.Recommendations.ToListAsync();
        }

        public async Task<Recommendation?> GetByIdAsync(int id)
        {
            return await _context.Recommendations.FindAsync(id);
        }

        public async Task<List<Recommendation>> GetAllByUserAsync(int userId)
        {
            return await _context.Recommendations.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<Recommendation>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Recommendations
                .Where(r => r.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
