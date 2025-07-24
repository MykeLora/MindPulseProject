using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _questionService.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var response = await _questionService.GetAllWithDetailsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _questionService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("by-questionnaire/{questionnaireId}")]
        public async Task<IActionResult> GetByQuestionnaireId(int questionnaireId)
        {
            var response = await _questionService.GetByQuestionnaireIdAsync(questionnaireId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("by-type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var response = await _questionService.GetByTypeAsync(type);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionCreateDTO dto)
        {
            var response = await _questionService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuestionUpdateDTO dto)
        {
            var response = await _questionService.UpdateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _questionService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
