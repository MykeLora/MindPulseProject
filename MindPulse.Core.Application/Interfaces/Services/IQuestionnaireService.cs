using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IQuestionnaireService : IGenericService<QuestionnaireCreateDTO, QuestionnaireUpdateDTO, Questionnaire, QuestionnaireResponseDTO>
    {
        Task<ApiResponse<List<QuestionnaireResponseDTO>>> GetAllWithQuestionsAndOptions();
        Task<ApiResponse<List<QuestionnaireSimpleDTO>>> GetAllSimpleAsync();
        Task<ApiResponse<QuestionnaireResponseDTO?>> GetByTitleAsync(string title);
        Task<ApiResponse<bool>> ExistsAsync(int id);

    }
}
