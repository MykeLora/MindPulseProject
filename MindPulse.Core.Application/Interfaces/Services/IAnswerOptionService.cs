using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IAnswerOptionService
    {
        Task<ApiResponse<List<AnswerOptionResponseDTO>>> GetAllAsync();
        Task<ApiResponse<AnswerOptionResponseDTO>> GetByIdAsync(int id);
        Task<ApiResponse<AnswerOptionResponseDTO>> CreateAsync(AnswerOptionCreateDTO dto);
        Task<ApiResponse<AnswerOptionResponseDTO>> UpdateAsync(int id, AnswerOptionUpdateDTO dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<List<AnswerOptionResponseDTO>>> GetByQuestionIdAsync(int questionId);
    }
}
