using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext context)
        {
            _dbContext = context;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entityList = await _dbContext.Set<T>().ToListAsync();
            return entityList;
        }

        public virtual async Task<List<T>> GetAllWithIncludes(List<string> properties)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (var property in properties)
            {
                query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity, int id)
        {
            var entry = await GetByIdAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
