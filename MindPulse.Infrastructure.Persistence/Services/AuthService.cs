using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Email;
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
using System.Threading.Tasks;
using System.Web;

namespace MindPulse.Core.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IUserRepository userRepository, IEmailService emailService,
            IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings
            , IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ApiResponse<bool>> ConfirmAccountAsync(string token)
        {
            var user = await _userRepository.GetByTokenAsync(token);

            if (user == null)
            {
                return new ApiResponse<bool>(404, "Token inválido o usuario no encontrado.");
            }

            if (user.IsConfirmed)
            {
                return new ApiResponse<bool>(400, "La cuenta ya está confirmada.");
            }

            user.IsConfirmed = true;
            user.VerificationToken = null;

            await _userRepository.UpdateAsync(user); 

            return new ApiResponse<bool>(200, "Cuenta confirmada exitosamente.");
        }

        public async Task<ApiResponse<string>> LoginAsync(UserLoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.Password)
            {
                return new ApiResponse<string>(401, "Credenciales inválidas");
            }

            if (!user.IsConfirmed)
            {
                return new ApiResponse<string>(403, "La cuenta aún no ha sido confirmada.");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiResponse<string>(200, tokenString);
        }

        public async Task<ApiResponse<string>> UserRegistrationAsync(UserRegistrationDTO userDto)
        {
            
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return new ApiResponse<string>(400, "El correo ya está registrado.");
            }

            
            var confirmationToken = Guid.NewGuid().ToString();
            var passwaord = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var newUser = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                UserName = userDto.UserName,
                PasswordHash = passwaord,
                Created = DateTime.UtcNow,
                IsConfirmed = false,
                VerificationToken = confirmationToken
            };

             await _userRepository.AddAsync(newUser);

            var backendUrl = _configuration["AppSettings:BackendUrl"];

            var confirmationLink = $"{confirmationToken}/api/users/confirmation?token={HttpUtility.UrlEncode(newUser.VerificationToken)}";
            var emailBody = $"<h2>Bienvenido/a {userDto.UserName}</h2><p>Por favor, confirma tu cuenta haciendo clic en el siguiente enlace:</p><a href='{confirmationLink}'>Confirmar cuenta</a>";

            var emailRequest = new EmailRequest
            {
                To = userDto.Email,
                Subject = "Confirma tu cuenta",
                Body = emailBody
            };

            await _emailService.SendAsync(emailRequest);

            return new ApiResponse<string>(201, "Registro exitoso. Por favor revisa tu correo para confirmar la cuenta.");
        }

    }
}