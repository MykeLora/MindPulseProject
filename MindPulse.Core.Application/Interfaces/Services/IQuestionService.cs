using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IQuestionService : IGenericService<QuestionCreateDTO, QuestionUpdateDTO, Question, QuestionResponseDTO>
    {
        Task<ApiResponse<List<QuestionResponseDTO>>> GetAllWithDetailsAsync();
        Task<ApiResponse<List<QuestionResponseDTO>>> GetByTypeAsync(string type);
        Task<ApiResponse<List<QuestionResponseDTO>>> GetByQuestionnaireIdAsync(int questionnaireId);

    }
}
