using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface ITestResultRepository
    {
        Task<TestResult> AddAsync(TestResult result);
        Task<TestResult?> GetByIdAsync(int id);
        Task<List<TestResult>> GetAllByUserAsync(int userId);
    }
}
