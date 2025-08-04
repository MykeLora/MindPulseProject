using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations.UserResponse;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Infrastructure.Persistence.Context;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class ChatController : ControllerBase
    {
        private readonly IFreeTextOrchestrationService _orchestrationService;
        private readonly IUserResponseService _service;
        private readonly ApplicationContext _context;
        public ChatController(IFreeTextOrchestrationService orchestrationService, IUserResponseService service, ApplicationContext context)
        {
            _orchestrationService = orchestrationService;
            _context = context;
            _service = service;
        }

        [HttpGet("chat-history/{userId}")]
        public async Task<IActionResult> GetChatHistory(int userId)
        {
            var result = await _orchestrationService.GetFullChatAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("chat-service")]
        public async Task<IActionResult> Create([FromBody] UserResponseCreateDTO dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("latest-emotion-alert")]
        public async Task<IActionResult> GetLatestAlert(int userId)
        {
            var latest = await _context.EmotionAnalyses
                .Where(e => e.UserId == userId && (e.DetectedEmotion.ToLower() == "moderado" || e.DetectedEmotion.ToLower() == "alto"))
                .OrderByDescending(e => e.AnalysisDate)
                .FirstOrDefaultAsync();

            if (latest == null)
                return Ok(new { alert = false });

            return Ok(new
            {
                alert = true,
                emotion = latest.DetectedEmotion,
                confidence = latest.Confidence,
                summary = latest.Summary,
                date = latest.AnalysisDate
            });
        }
    }
}
