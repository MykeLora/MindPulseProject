using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerOptionController : ControllerBase
    {
        private readonly IAnswerOptionService _answerOptionService;

        public AnswerOptionController(IAnswerOptionService answerOptionService)
        {
            _answerOptionService = answerOptionService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _answerOptionService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _answerOptionService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AnswerOptionCreateDTO dto)
        {
            var result = await _answerOptionService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnswerOptionUpdateDTO dto)
        {
            var result = await _answerOptionService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _answerOptionService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-question/{questionId}")]
        public async Task<IActionResult> GetByQuestionId(int questionId)
        {
            var result = await _answerOptionService.GetByQuestionIdAsync(questionId);
            return StatusCode(result.StatusCode, result);
        }

    }
}
