using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IAiResponseService
    {
        Task<ApiResponse<AiResponseDTO>> CreateAsync(AiResponseCreateDTO aiResponseCreateDto);
        Task<ApiResponse<List<AiResponseDTO>>> GetByUserIdAsync(int userId);
    }
}
