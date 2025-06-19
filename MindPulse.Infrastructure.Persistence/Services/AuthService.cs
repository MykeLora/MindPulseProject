using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Email;
using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Settings;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MindPulse.Core.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<DTOs.Auth.UserResponseDTO>> UserRegistrationAsync(UserRegistrationDTO usuarioDto)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(usuarioDto.Email);
                if (existingUser != null)
                {
                    return new ApiResponse<DTOs.Auth.UserResponseDTO>(400, "Email already in use.");
                }

                var user = new User
                {
                    Name = usuarioDto.Name,
                    UserName = usuarioDto.UserName,
                    Email = usuarioDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password),
                    Created = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);

                var userResponse = new DTOs.Auth.UserResponseDTO
                {
                    Id = user.Id,
                    Name = $"{user.Name}",
                    Email = user.Email
                };

                return new ApiResponse<DTOs.Auth.UserResponseDTO>(200, userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                return new ApiResponse<DTOs.Auth.UserResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<LoginResponseDTO>> LoginAsync(UserLoginDTO loginDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return new ApiResponse<LoginResponseDTO>(401, "Invalid email or password.");
                }

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    return new ApiResponse<LoginResponseDTO>(401, "Invalid email or password.");
                }

                var loginResponse = new LoginResponseDTO
                {
                    Message = "Login successful.",
                    UserId = user.Id,
                    UserName = $"{user.Name}"
                };

                return new ApiResponse<LoginResponseDTO>(200, loginResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in.");
                return new ApiResponse<LoginResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(changePasswordDto.Id);
                if (user == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "User not found.");
                }

                user.PasswordHash = changePasswordDto.NewPassword; 
                await _userRepository.ChangePasswordAsync(user);

                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                {
                    Message = "Password changed successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password.");
                return new ApiResponse<ConfirmationResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }
    }

}