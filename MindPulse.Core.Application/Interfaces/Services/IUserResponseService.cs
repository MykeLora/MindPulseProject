using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IUserResponseService
    {
        Task<ApiResponse<UserResponsesDTO>> CreateAsync(UserResponseCreateDTO dto);
        Task<ApiResponse<List<UserResponsesDTO>>> GetByUserAsync(int userId);
        Task<ApiResponse<List<UserResponsesDTO>>> GetFreeResponsesAsync(int userId);
    }
}
