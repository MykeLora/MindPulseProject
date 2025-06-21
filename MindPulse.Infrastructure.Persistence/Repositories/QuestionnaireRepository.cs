using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class QuestionnaireRepository : GenericRepository<Questionnaire>,IQuestionaireRepository
    {

        private readonly ApplicationContext _context;

        public QuestionnaireRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Questionnaire> GetWithQuestionsByIdAsync(int id)
        {
            return await _context.Questionnaires
                .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Questionnaire>> GetAllWithQuestionsAsync()
        {
            return await _context.Questionnaires
                .Include(q => q.Questions)
                  .ThenInclude(q => q.AnswerOptions)
                .ToListAsync();
        }

    }
}
