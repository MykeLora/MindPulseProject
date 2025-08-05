using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers.Recommendations
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EducationalContentController : ControllerBase
    {
        private readonly IEducationalContentService _educationalContentService;

        public EducationalContentController(IEducationalContentService educationalContentService)
        {
            _educationalContentService = educationalContentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] EducationalContentCreateDTO dto)
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

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _educationalContentService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _educationalContentService.GetByCategoryIdAsync(categoryId);
            return StatusCode(result.StatusCode, result);
        }


        // Endpoints for Admin Roles

        //[Authorize(Roles = "Admin")]
        //[HttpPut("update/{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] EducationalContentDTO dto)
        //{
        //    if (id != dto.Id) return BadRequest("ID mismatch");
        //    var result = await _educationalContentService.UpdateAsync(dto);
        //    return StatusCode(result.StatusCode, result);
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _educationalContentService.DeleteAsync(id);
        //    return StatusCode(result.StatusCode, result);
        //}
    }
}
