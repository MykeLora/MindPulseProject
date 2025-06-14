using MindPulse.Core.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface AuthService
    {
        Task<string> UserRegistrationAsync(UserRegistrationDTO usuarioDto);
        Task<string> LoginAsync(UserLoginDTO loginDto);
        Task<bool> ConfirmAccountAsync(string token);

    }
}
