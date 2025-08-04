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
    public class AnswerOptionRepository : IAnswerOptionRepository
    {
        private readonly ApplicationContext _context;

        public AnswerOptionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<AnswerOption>> GetAllAsync()
        {
            return await _context.AnswerOptions.ToListAsync();
        }

        public async Task<AnswerOption?> GetByIdAsync(int id)
        {
            return await _context.AnswerOptions.FindAsync(id);
        }

        public async Task<AnswerOption> AddAsync(AnswerOption entity)
        {
            await _context.AnswerOptions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AnswerOption> UpdateAsync(AnswerOption entity)
        {
            _context.AnswerOptions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AnswerOptions.FindAsync(id);
            if (entity == null)
                return false;

            _context.AnswerOptions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
