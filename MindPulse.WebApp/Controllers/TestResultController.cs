using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Infrastructure.Shared.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _testResultService;

        public TestResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TestResultCreateDTO dto)
        {
            var result = await _testResultService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _testResultService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            var result = await _testResultService.GetAllByUserAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testResultService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
