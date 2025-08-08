using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.Interfaces.Services;

namespace MindPulse.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class ConfidenceController : ControllerBase
    {
        private readonly IConfidenceService _confidenceService;
        public ConfidenceController(IConfidenceService confidenceService)
        {
            _confidenceService = confidenceService;
        }

        /// <summary>
        /// Devuelve la confianza diaria del usuario especificado.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Diaria del Usuario</returns>
        [HttpGet("daily/{userId}")]
        public async Task<IActionResult> GetDaily(int userId)
        {
            var result = await _confidenceService.GetDailyConfidenceByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Devuelve la confianza semanal del usuario especificado.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Semanal del Usuario</returns>
        [HttpGet("weekly/{userId}")]
        public async Task<IActionResult> GetWeekly(int userId)
        {
            var result = await _confidenceService.GetWeeklyConfidenceByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Devuelve la confianza mensual del usuario especificado.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Mensual del Usuario</returns>
        [HttpGet("monthly/{userId}")]
        public async Task<IActionResult> GetMonthly(int userId)
        {
            var result = await _confidenceService.GetMonthlyConfidenceByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Devuelve la confianza global del usuario especificado.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Global del Usuario</returns>
        [HttpGet("global/{userId}")]
        public async Task<IActionResult> GetGlobal(int userId)
        {
            var result = await _confidenceService.GetGlobalConfidenceByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
