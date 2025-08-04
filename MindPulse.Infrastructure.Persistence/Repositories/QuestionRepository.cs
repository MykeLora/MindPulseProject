using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<QuestionRepository> _logger;

        public QuestionRepository(ApplicationContext context, ILogger<QuestionRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<List<Question>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _context.Questions
                .Where(q => q.QuestionnaireId == questionnaireId)
                .Include(q => q.AnswerOptions) 
                .ToListAsync();
        }


        public async Task<List<Question>> GetByTypeAsync(string type)
        {
            return await _context.Questions
                .Where(q => q.Type == type)
                .ToListAsync();
        }

        public async Task<Question> GetWithOptionsByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.AnswerOptions)
                .Include(q => q.Questionnaire) 
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
