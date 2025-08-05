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
    public class TestAnalysisController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerOptionService _answerOptionService;

        public TestAnalysisController(
            IEvaluationService evaluationService,
            ICategoryService categoryService,
            IQuestionnaireService questionnaireService,
            IQuestionService questionService,
            IAnswerOptionService answerOptionService)
        {
            _evaluationService = evaluationService;
            _categoryService = categoryService;
            _questionnaireService = questionnaireService;
            _questionService = questionService;
            _answerOptionService = answerOptionService;
        }

        [HttpPost("submit-test-preview")]
        public async Task<IActionResult> SubmitTestPreview([FromBody] TestResponseDTO input)
        {
            // Validamos que se envíen respuestas
            if (input.Answers == null || !input.Answers.Any())
            return BadRequest("Debes enviar al menos una respuesta.");

            // Obtenemos el nombre de la categoría
            var category = await _categoryService.GetByIdAsync(input.CategoryId);
            if (!category.Success || category.Data == null)
            {
                return NotFound(new ApiResponse<string>(404, "Categoría no encontrada."));
            }

            // Obtenemos el nombre del cuestionario
            var questionnaire = await _questionnaireService.GetByIdAsync(input.QuestionnaireId);
            if (!questionnaire.Success || questionnaire.Data == null)
            {
                return NotFound(new ApiResponse<string>(404, "Cuestionario no encontrado."));
            }

            // Obtenemos los textos de preguntas y respuestas
            var answerDetails = new List<AnswerDetailDTO>();
            foreach (var pair in input.Answers)
            {
                var question = await _questionService.GetByIdAsync(pair.QuestionId);
                var answer = await _answerOptionService.GetByIdAsync(pair.AnswerOptionId);

                if (question.Data == null || answer.Data == null)
                    continue;

                answerDetails.Add(new AnswerDetailDTO
                {
                    QuestionText = question.Data.Text,
                    AnswerText = answer.Data.Text
                });
            }

            // Devolver datos enriquecidos
            var enriched = new TestSubmissionDTO
            {
                UserId = input.UserId,
                CategoryName = category.Data.Name,
                QuestionnaireName = questionnaire.Data.Title,
                Answers = answerDetails
            };

            return Ok(enriched);
        }
    }
}
