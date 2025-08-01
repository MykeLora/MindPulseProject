using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Entity>> GetAllWithIncludes(List<string> properties);
        Task<List<Entity>> FindAsync(Expression<Func<Entity, bool>> predicate);
    }
}
