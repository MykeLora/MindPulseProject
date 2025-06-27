using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.User;
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
        Task<ApiResponse<DTOs.Auth.UserResponseDTO>> UserRegistrationAsync(UserRegistrationDTO usuarioDto);
        Task<ApiResponse<LoginResponseDTO>> LoginAsync(UserLoginDTO loginDto);
        Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto);
        Task<ApiResponse<ConfirmationResponseDTO>> ForgotPasswordAsync(ForgotPasswordRequestDTO forgotPasswordDto);
        Task<ApiResponse<ConfirmationResponseDTO>> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto);

    }
}
