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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApplicationContext _context;

        public TestResultRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TestResult> AddAsync(TestResult result)
        {
            _context.TestResults.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<TestResult?> GetByIdAsync(int id)
        {
            return await _context.TestResults.FindAsync(id);
        }

        public async Task<List<TestResult>> GetAllByUserAsync(int userId)
        {
            return await _context.TestResults.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}
