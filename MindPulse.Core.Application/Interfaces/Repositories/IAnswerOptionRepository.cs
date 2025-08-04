using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IAnswerOptionRepository
    {
        Task<List<AnswerOption>> GetAllAsync();
        Task<AnswerOption?> GetByIdAsync(int id);
        Task<AnswerOption> AddAsync(AnswerOption entity);
        Task<AnswerOption> UpdateAsync(AnswerOption entity);
        Task<bool> DeleteAsync(int id);
    }

}
