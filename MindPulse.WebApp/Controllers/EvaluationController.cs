using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Evaluations.TestingPurposes;
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
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IQuestionService _questionService;
        // IAnswerOptionService _answerOptionService;

        public EvaluationController(IEvaluationService evaluationService, IFreeTextOrchestrationService orchestrationService, ICategoryService categoryService)
        {
            _evaluationService = evaluationService;
            _freeTextOrchestrationService = orchestrationService;
            _categoryService = categoryService;
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

        // Testing Purposes
        [HttpPost("test-(prueba)")]
        public async Task<IActionResult> EvaluateTest([FromBody] TestEvaluationRequest request)
        {
            var result = await _evaluationService.EvaluateTestAsync(request);
            return Ok(result);
        }

        [HttpPost("submit-test-(prueba)")]
        public IActionResult SubmitTest([FromBody] TestResponsesDTO dto)
        {
            if (dto.Answers == null || !dto.Answers.Any())
                return BadRequest("Debes enviar al menos una respuesta.");

            // Mostrar en consola para pruebas
            Console.WriteLine($"Usuario: {dto.UserId}");
            Console.WriteLine($"Cuestionario: {dto.QuestionnaireId}");
            foreach (var ans in dto.Answers)
            {
                Console.WriteLine($"Pregunta {ans.QuestionId}: Opción {ans.AnswerId}");
            }

            return Ok(new { message = "Respuestas recibidas correctamente." });
        }

        //[HttpPost("submit-test-(preview)")]
        //public async Task<IActionResult> SubmitTestPreview([FromBody] TestCreateDTO input)
        //{
        //    // Obtenemos el nombre de la categoría
        //    var category = await _categoryService.GetByIdAsync(input.CategoryId);
        //    if (!category.Success || category.Data == null)
        //    {
        //        return NotFound(new ApiResponse<string>(404, "Categoría no encontrada."));
        //    }

        //    // Obtenemos el nombre del cuestionario
        //    var questionnaire = await _questionnaireService
        //}
    }
}
