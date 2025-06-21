using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<List<Question>> GetByQuestionnaireIdAsync(int questionnaireId);
        Task<Question> GetWithOptionsByIdAsync(int id);
        Task<List<Question>> GetByTypeAsync(string type);



    }
}
