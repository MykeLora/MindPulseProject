using MindPulse.Core.Application.DTOs.Confidence;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Services
{
    public class ConfidenceService : IConfidenceService
    {
        private readonly IConfidenceRepository _confidenceRepository;

        public ConfidenceService(IConfidenceRepository confidenceRepository)
        {
            _confidenceRepository = confidenceRepository;
        }

        /// <summary>
        /// Calcula la confianza diaria del usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Diaria.</returns>
        public async Task<ApiResponse<ConfidenceDailyDTO>> GetDailyConfidenceByUserIdAsync(int userId)
        {
            if (userId <= 0)
                return new ApiResponse<ConfidenceDailyDTO>(400, "El UserId debe ser mayor que 0.");

            var result = await _confidenceRepository.GetDailyConfidenceByUserIdAsync(userId);
            return new ApiResponse<ConfidenceDailyDTO>(200, result);
        }

        /// <summary>
        /// Calcula la confianza semanal del usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Semanal.</returns>
        public async Task<ApiResponse<ConfidenceWeeklyDTO>> GetWeeklyConfidenceByUserIdAsync(int userId)
        {
            if (userId <= 0)
                return new ApiResponse<ConfidenceWeeklyDTO>(400, "El UserId debe ser mayor que 0.");

            var result = await _confidenceRepository.GetWeeklyConfidenceByUserIdAsync(userId);
            return new ApiResponse<ConfidenceWeeklyDTO>(200, data: result);
        }

        /// <summary>
        /// Calcula la confianza mensual del usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Mensual.</returns>
        public async Task<ApiResponse<ConfidenceMontlyDTO>> GetMonthlyConfidenceByUserIdAsync(int userId)
        {
            if (userId <= 0)
                return new ApiResponse<ConfidenceMontlyDTO>(400, "El UserId debe ser mayor que 0.");

            var result = await _confidenceRepository.GetMonthlyConfidenceByUserIdAsync(userId);
            return new ApiResponse<ConfidenceMontlyDTO>(200, data: result);
        }

        /// <summary>
        /// Calcula la confianza global del usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Global.</returns>
        public async Task<ApiResponse<ConfidenceGlobalDTO>> GetGlobalConfidenceByUserIdAsync(int userId)
        {
            if (userId <= 0)
                return new ApiResponse<ConfidenceGlobalDTO>(400, "El UserId debe ser mayor que 0.");

            var result = await _confidenceRepository.GetGlobalConfidenceByUserIdAsync(userId);
            return new ApiResponse<ConfidenceGlobalDTO>(200, data: result);
        }

    }
}
