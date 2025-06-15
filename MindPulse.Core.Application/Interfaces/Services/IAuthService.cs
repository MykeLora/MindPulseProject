using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> UserRegistrationAsync(UserRegistrationDTO usuarioDto);
        Task<ApiResponse<string>> LoginAsync(UserLoginDTO loginDto);
        Task<ApiResponse<bool>> ConfirmAccountAsync(string token);

    }
}
