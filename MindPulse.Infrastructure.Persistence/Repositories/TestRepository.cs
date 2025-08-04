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
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationContext _context;
        public TestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Test> AddAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<List<Test>> GetAllByUserAsync(int userId)
        {
            return await _context.Tests.Where(t => t.UserId == userId).ToListAsync();
        }

    }
}
