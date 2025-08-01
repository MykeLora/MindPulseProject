using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return false; 

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true; 
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

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

    }

}


    
