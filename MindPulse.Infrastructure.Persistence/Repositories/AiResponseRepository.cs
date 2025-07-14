using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class AiResponseRepository : GenericRepository<AiResponse>, IAiResponseRepository
    {
        private readonly ApplicationContext _context;
        public AiResponseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AiResponse>> GetByUserIdAsync(int userId)
        {
            return await _context.AiResponses
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
