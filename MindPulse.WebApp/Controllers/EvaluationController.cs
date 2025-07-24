using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Infrastructure.Shared.Services;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        private readonly IFreeTextOrchestrationService _freeTextOrchestrationService;

        public EvaluationController(IEvaluationService evaluationService, IFreeTextOrchestrationService orchestrationService)
        {
            _evaluationService = evaluationService;
            _freeTextOrchestrationService = orchestrationService;
        }

        /// <summary>
        /// Eval�a un test emocional estructurado con respuestas espec�ficas.
        /// </summary>
        /// <param name="request">Datos del test emocional.</param>
        /// <returns>Resultado del an�lisis emocional.</returns>
        //[HttpPost("test")]
        //public async Task<IActionResult> EvaluateTest([FromBody] EvaluationRequest request)
        //{
        //    var result = await _evaluationService.EvaluateTestAsync(request);
        //    return Ok(result);
        //}

        ///// <summary>
        ///// Eval�a una conversaci�n de texto libre (entrada emocional).
        ///// </summary>
        ///// <param name="request">Mensajes escritos por el usuario.</param>
        ///// <returns>Resultado del an�lisis emocional.</returns>
        [HttpPost("free-text-analysis")]
        public async Task<IActionResult> AnalyzeFromText([FromBody] UserResponseCreateDTO input)
        {
            if (string.IsNullOrWhiteSpace(input.FreeResponse))
                return BadRequest(new { error = "El texto no puede estar vac�o." });

            var aiResponse = await _freeTextOrchestrationService.AnalyzeAndStoreAsync(input.UserId, input.FreeResponse);
            return Ok(new ApiResponse<string>(200, aiResponse));
        }
    }
}
