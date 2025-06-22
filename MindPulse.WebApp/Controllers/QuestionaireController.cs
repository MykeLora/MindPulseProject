using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        [HttpGet("Get-All-Questionaire")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _questionnaireService.GetAllSimpleAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("with-full-details")]
        public async Task<IActionResult> GetAllWithFullDetails()
        {
            var response = await _questionnaireService.GetAllWithQuestionsAndOptions();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("with-questions")]
        public async Task<IActionResult> GetAllWithQuestions()
        {
            var response = await _questionnaireService.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetQuestionaireById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _questionnaireService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var response = await _questionnaireService.GetByTitleAsync(title);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Create-Questionaire")]
        public async Task<IActionResult> Create([FromBody] QuestionnaireCreateDTO dto)
        {
            var response = await _questionnaireService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update-Questionaire")]
        public async Task<IActionResult> Update([FromBody] QuestionnaireUpdateDTO dto)
        {
            var response = await _questionnaireService.UpdateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("RemoveById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _questionnaireService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> Exists(int id)
        {
            var response = await _questionnaireService.ExistsAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
