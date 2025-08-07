using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class EmotionalHistoriesController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;
        public EmotionalHistoriesController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        // GetAllByUser
        // GetWeeklyByUser
        // GetMonthlyByUser
        // GetGlobalByUser
    }
}
