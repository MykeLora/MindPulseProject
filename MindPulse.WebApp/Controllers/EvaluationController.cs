using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.UserResponse;
using MindPulse.Core.Application.DTOs.Orchestrations;
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
        //private readonly ICategoryService _categoryService;
        //private readonly IQuestionnaireService _questionnaireService;
        //private readonly IQuestionService _questionService;
        //private readonly IAnswerOptionService _answerOptionService;

        public EvaluationController(
            IEvaluationService evaluationService, 
            IFreeTextOrchestrationService orchestrationService 
            //ICategoryService categoryService, 
            //IQuestionnaireService questionnaireService, 
            //IQuestionService questionService,
            //IAnswerOptionService answerOptionService
            )
        {
            _evaluationService = evaluationService;
            _freeTextOrchestrationService = orchestrationService;
            //_categoryService = categoryService;
            //_questionnaireService = questionnaireService;
            //_questionService = questionService;
            //_answerOptionService = answerOptionService;
        }

        ///// <summary>
        ///// Evalúa una conversación de texto libre (entrada emocional).
        ///// </summary>
        ///// <param name="request">Mensajes escritos por el usuario.</param>
        ///// <returns>Resultado del análisis emocional.</returns>
        [HttpPost("free-text-analysis")]
        public async Task<IActionResult> AnalyzeFromText([FromBody] UserResponseCreateDTO input)
        {
            if (string.IsNullOrWhiteSpace(input.FreeResponse))
                return BadRequest(new { error = "El texto no puede estar vacío." });

            var aiResponse = await _freeTextOrchestrationService.AnalyzeAndStoreAsync(input.UserId, input.FreeResponse);
            return Ok(new ApiResponse<string>(200, data: aiResponse));
        }

        /// <summary>
        /// Evalúa un test emocional estructurado con respuestas específicas.
        /// </summary>
        /// <param name="request">Datos del test emocional.</param>
        /// <returns>Resultado del análisis emocional.</returns>
        /// 
        /// A desarrollar. 
    }
}
