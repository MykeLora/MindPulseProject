using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        /// <summary>
        /// Evalúa un test emocional estructurado con respuestas específicas.
        /// </summary>
        /// <param name="request">Datos del test emocional.</param>
        /// <returns>Resultado del análisis emocional.</returns>
        [HttpPost("test")]
        public async Task<IActionResult> EvaluateTest([FromBody] EvaluationRequest request)
        {
            var result = await _evaluationService.EvaluateTestAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Evalúa una conversación de texto libre (entrada emocional).
        /// </summary>
        /// <param name="request">Mensajes escritos por el usuario.</param>
        /// <returns>Resultado del análisis emocional.</returns>
        [HttpPost("freetext")]
        public async Task<IActionResult> EvaluateFreeText([FromBody] FreeTextEvaluationRequest request)
        {
            var result = await _evaluationService.EvaluateFreeTextAsync(request);
            return Ok(result);
        }
    }
}
