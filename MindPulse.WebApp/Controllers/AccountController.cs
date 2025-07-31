using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using UserResponseDTO = MindPulse.Core.Application.DTOs.Auth.UserResponseDTO;

namespace MindPulse.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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



        /// <summary>
        /// CRUD para la gestión de usuarios por parte de administradores
        /// </summary>
        /// 

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            var response = await _userService.GetAllAsync();

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserAdminCreateDTO dto)
        {
            var response = await _userService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update([FromBody] UserAdminUpdateDTO dto)
        {
            var response = await _userService.UpdateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return NoContent();
        }


    }
}

