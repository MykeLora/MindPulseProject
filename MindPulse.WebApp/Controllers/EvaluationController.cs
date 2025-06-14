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
        /// Eval�a un test emocional estructurado con respuestas espec�ficas.
        /// </summary>
        /// <param name="request">Datos del test emocional.</param>
        /// <returns>Resultado del an�lisis emocional.</returns>
        [HttpPost("test")]
        public async Task<IActionResult> EvaluateTest([FromBody] EvaluationRequest request)
        {
            var result = await _evaluationService.EvaluateTestAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Eval�a una conversaci�n de texto libre (entrada emocional).
        /// </summary>
        /// <param name="request">Mensajes escritos por el usuario.</param>
        /// <returns>Resultado del an�lisis emocional.</returns>
        [HttpPost("freetext")]
        public async Task<IActionResult> EvaluateFreeText([FromBody] FreeTextEvaluationRequest request)
        {
            var result = await _evaluationService.EvaluateFreeTextAsync(request);
            return Ok(result);
        }
    }
}
