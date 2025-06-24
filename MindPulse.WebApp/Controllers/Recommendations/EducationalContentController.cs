using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers.Recommendations
{
    [ApiController]
    [Route("api/educational-content")]
    public class EducationalContentController : ControllerBase
    {
        private readonly IEducationalContentService _educationalContentService;

        public EducationalContentController(IEducationalContentService educationalContentService)
        {
            _educationalContentService = educationalContentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] EducationalContentDTO dto)
        {
            var result = await _educationalContentService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _educationalContentService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _educationalContentService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EducationalContentDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var result = await _educationalContentService.UpdateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _educationalContentService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
