using MindPulse.Core.Application.DTOs.Confidence;
using MindPulse.Core.Domain.Entities.Emotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IConfidenceRepository
    {
        Task<ConfidenceDailyDTO> GetDailyConfidenceByUserIdAsync(int userId);
        Task<ConfidenceWeeklyDTO> GetWeeklyConfidenceByUserIdAsync(int userId);
        Task<ConfidenceMontlyDTO> GetMonthlyConfidenceByUserIdAsync(int userId);
        Task<ConfidenceGlobalDTO> GetGlobalConfidenceByUserIdAsync(int userId);

    }
}
