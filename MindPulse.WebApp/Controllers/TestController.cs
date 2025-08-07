using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Infrastructure.Shared.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TestCreateDTO dto)
        {
            var result = await _testService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _testService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            var result = await _testService.GetAllByUserAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("submit-new-test")]
        public async Task<IActionResult> SubmitTest([FromBody] TestResponseDTO input)
        {
            var result = await _testService.SubmitTestAsync(input);
            return Ok(new { message = "Respuestas recibidas correctamente." });
        }
    }
}
