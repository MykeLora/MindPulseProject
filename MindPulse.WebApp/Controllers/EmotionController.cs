using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmotionController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;
        public EmotionController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeEmotion([FromBody] EmotionRequest request)
        {
            var result = await _openAiService.AnalyzeTextAsync(request.Text);
            return Ok(result);
        }
    }
}
