using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class ChatController : ControllerBase
    {
        private readonly IFreeTextOrchestrationService _orchestrationService;
        public ChatController(IFreeTextOrchestrationService orchestrationService)
        {
            _orchestrationService = orchestrationService;
        }

        [HttpGet("chat-history/{userId}")]
        public async Task<IActionResult> GetChatHistory(int userId)
        {
            var result = await _orchestrationService.GetFullChatAsync(userId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
