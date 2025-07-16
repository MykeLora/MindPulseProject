using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class UserResponseController : ControllerBase
    {
        private readonly IUserResponseService _service;
        public UserResponseController(IUserResponseService service)
        {
            _service = service;
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var response = await _service.GetByUserAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("free-responses")]
        public async Task<IActionResult> GetFreeResponses(int userId)
        {
            var response = await _service.GetFreeResponsesAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("chat-service")]
        public async Task<IActionResult> Create([FromBody] UserResponseCreateDTO dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }

}