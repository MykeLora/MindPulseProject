using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Infrastructure.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindPulse.WebApp.Controllers.Recommendations
{
    [Authorize]  
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] RecommendationCreateDTO dto)
        {
            var result = await _recommendationService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recommendationService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _recommendationService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            var result = await _recommendationService.GetAllByUserAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _recommendationService.GetByCategoryIdAsync(categoryId);
            return StatusCode(result.StatusCode, result);
        }


        // Endpoints for Admin roles

        //[HttpPut("update/{id}")]
        //[Authorize(Roles = "Admin")] 
        //public async Task<IActionResult> Update(int id, [FromBody] RecommendationDTO dto)
        //{
        //    if (id != dto.Id)
        //        return BadRequest("ID mismatch");

        //    var result = await _recommendationService.UpdateAsync(dto);
        //    return StatusCode(result.StatusCode, result);
        //}

        //[HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")] 
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _recommendationService.DeleteAsync(id);
        //    return StatusCode(result.StatusCode, result);
        //}

    }
}
