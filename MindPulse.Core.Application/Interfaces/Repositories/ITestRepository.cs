using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface ITestRepository
    {
        Task<Test> AddAsync(Test test);
        Task<Test?> GetByIdAsync(int id);
        Task<List<Test>> GetAllByUserAsync(int userId);
    }
}
