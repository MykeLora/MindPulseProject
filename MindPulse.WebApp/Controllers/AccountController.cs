using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// Cambiar la contraseña de un usuario
        /// </summary>
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
