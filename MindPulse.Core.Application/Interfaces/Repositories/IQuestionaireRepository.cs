using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IQuestionaireRepository : IGenericRepository<Questionnaire>
    {
        Task<Questionnaire> GetWithQuestionsByIdAsync(int id);
        Task<List<Questionnaire>> GetAllWithQuestionsAsync();


    }
}
