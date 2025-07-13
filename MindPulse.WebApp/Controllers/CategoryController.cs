using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryDTO dto)
        {
            var response = await _categoryService.CreateAsync(dto);

            if (!response.Success)
                return BadRequest(new { message = response.Errors });

            return StatusCode(response.StatusCode, response);
        }

 
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

  
        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var response = await _categoryService.UpdateAsync(dto);
            if (!response.Success)
                return BadRequest(new { message = response.Errors });

            return StatusCode(response.StatusCode, response);
        }

   
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
