using MindPulse.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IFreeTextOrchestrationService
    {
        Task<EvaluationResult> AnalyzeAndStoreAsync(int userId, string input);
    }
}
