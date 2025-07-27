using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Email;
using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using AutoMapper;
using static System.Formats.Asn1.AsnWriter;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MindPulse.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public AuthService(
            IUserRepository userRepository,
            ILogger<AuthService> logger,
            IMapper mapper,
            IEmailService emailService,
            IJwtService jwtService, 
            IConfiguration config)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
            _jwtService = jwtService;
            _config = config;
        }

        public async Task<ApiResponse<Core.Application.DTOs.Auth.UserResponseDTO>> UserRegistrationAsync(UserRegistrationDTO usuarioDto)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(usuarioDto.Email);
                if (existingUser != null)
                    return new ApiResponse<Core.Application.DTOs.Auth.UserResponseDTO>(400, "Email already in use.");

                var user = _mapper.Map<User>(usuarioDto);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password);
                user.Created = DateTime.UtcNow;

                await _userRepository.AddAsync(user);

                // Send welcome/confirmation email
                var emailRequest = new EmailRequest
                {
                    To = user.Email,
                    Subject = "Welcome to MindPulse!",
                    Body = $"<p>Hello {user.Name},</p><p>Thank you for registering at MindPulse.</p>"
                };
                await _emailService.SendAsync(emailRequest);

                var userResponse = _mapper.Map<Core.Application.DTOs.Auth.UserResponseDTO>(user);

                return new ApiResponse<Core.Application.DTOs.Auth.UserResponseDTO>(200, userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                return new ApiResponse<Core.Application.DTOs.Auth.UserResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }


        public async Task<ApiResponse<LoginResponseDTO>> LoginAsync(UserLoginDTO loginDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
                if (user == null)
                    return new ApiResponse<LoginResponseDTO>(401, "Invalid email or password.");

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
                if (!isPasswordValid)
                    return new ApiResponse<LoginResponseDTO>(401, "Invalid email or password.");

                // Generar el token JWT con el rol del usuario
                var token = _jwtService.GenerateToken(
                    user.Id.ToString(),
                    user.Email,
                    new List<string> { user.Role.ToString() } // Rol como claim
                );

                var loginResponse = new LoginResponseDTO
                {
                    Message = "Login successful.",
                    UserId = user.Id,
                    UserName = user.Name,
                    Token = token,
                    Rol = user.Role.ToString()
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
                    return new ApiResponse<ConfirmationResponseDTO>(404, "User not found.");

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash);
                if (!isPasswordValid)
                    return new ApiResponse<ConfirmationResponseDTO>(401, "Invalid password.");

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

        public async Task<ApiResponse<ConfirmationResponseDTO>> ForgotPasswordAsync(ForgotPasswordRequestDTO forgotPasswordDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(forgotPasswordDto.Email);
                if (user == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "No existe un usuario con ese correo.");
                
                var token = _jwtService.GeneratePasswordResetToken(user.Id.ToString(), user.Email);

                // Enlace de recuperación (a modificar y conectar en front-end)
                var resetLink = $"{forgotPasswordDto.ResetUrl}?token={token}";

                var email = new EmailRequest
                {
                    To = user.Email,
                    Subject = "Recuperación de contraseña",
                    Body = $"""
                        Hola {user.Name},<br>
                        Hemos recibido una solicitud para restablecer tu contraseña.<br><br>
                        Haz clic en el siguiente enlace para establecer una nueva contraseña (válido por 1 hora):<br>
                        <a href="{resetLink}">Restablecer contraseña</a><br><br>
                        Si no solicitaste este cambio, por favor desestima este correo.<br><br>
                    """
                };

                await _emailService.SendAsync(email);

                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                {
                    Message = "Correo de recuperación enviado."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en recuperación de contraseña.");
                return new ApiResponse<ConfirmationResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }


        public async Task<ApiResponse<ConfirmationResponseDTO>> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto)
        {
            try
            {
                if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmNewPassword)
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Las contraseñas no coinciden.");

                var handler = new JwtSecurityTokenHandler();
                var jwtSettings = _config.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

                var principal = handler.ValidateToken(resetPasswordDto.Token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero 
                }, out _);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userId))
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Token inválido.");

                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null || user.Id.ToString() != userId)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Usuario no encontrado.");    

                user.PasswordHash = resetPasswordDto.NewPassword;
                await _userRepository.ChangePasswordAsync(user);

                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                {
                    Message = "Contraseña restablecida correctamente."
                });
            }

            catch (SecurityTokenExpiredException)
            {
                return new ApiResponse<ConfirmationResponseDTO>(401, "El enlace ha expirado. Por favor, solicita un nuevo enlace de recuperación.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting password.");
                return new ApiResponse<ConfirmationResponseDTO>(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
