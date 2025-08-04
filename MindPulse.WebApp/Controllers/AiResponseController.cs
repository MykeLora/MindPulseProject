using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations.AiResponse;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class AiResponseController : ControllerBase
    {
        private readonly IAiResponseService _aiResponseService;
        public AiResponseController(IAiResponseService aiResponseService)
        {
            _aiResponseService = aiResponseService;
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<AiResponseDTO>>>> GetByUserId(int userId)
        {
            var result = await _aiResponseService.GetByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
