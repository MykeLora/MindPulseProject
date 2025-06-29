using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using UserResponseDTO = MindPulse.Core.Application.DTOs.Auth.UserResponseDTO;

namespace MindPulse.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Iniciar sesión de usuario
        /// </summary>
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> LoginAsync([FromBody] UserLoginDTO loginDto)
        {
            if (loginDto == null)
                return BadRequest(new ApiResponse<LoginResponseDTO>(400, "Datos de inicio de sesión no válidos."));

            var response = await _authService.LoginAsync(loginDto);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        /// <summary>
        /// Registrar un nuevo usuario
        /// </summary>
        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> RegisterAsync([FromBody] UserRegistrationDTO registerDto)
        {
            if (registerDto == null)
                return BadRequest(new ApiResponse<UserResponseDTO>(400, "Datos de registro no válidos."));

            var response = await _authService.UserRegistrationAsync(registerDto);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        /// <summary>
        /// Recuperar contraseña de usuario (Solicitud de enlace para restablecimiento)
        /// </summary>
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ForgotPassword([FromBody] ForgotPasswordRequestDTO forgotPasswordDto)
        {
            if (forgotPasswordDto == null || string.IsNullOrWhiteSpace(forgotPasswordDto.Email))
                return BadRequest(new ApiResponse<ConfirmationResponseDTO>(400, "El email es requerido."));

            var response = await _authService.ForgotPasswordAsync(forgotPasswordDto);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        /// <summary>
        /// Restablecimiento a través del enlace enviado por correo
        /// </summary>

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto == null || string.IsNullOrWhiteSpace(resetPasswordDto.Token) || string.IsNullOrWhiteSpace(resetPasswordDto.NewPassword))
                return BadRequest(new ApiResponse<ConfirmationResponseDTO>(400, "Datos inválidos."));

            var response = await _authService.ResetPasswordAsync(resetPasswordDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Cambiar la contraseña de un usuario
        /// </summary>
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ChangePasswordAsync([FromBody] ChangePasswordDTO passwordDto)
        {
            if (passwordDto == null)
                return BadRequest(new ApiResponse<ConfirmationResponseDTO>(400, "Datos inválidos para cambio de contraseña."));

            var response = await _authService.ChangePasswordAsync(passwordDto);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }
    }
}

