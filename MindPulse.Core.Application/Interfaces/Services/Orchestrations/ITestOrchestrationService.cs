using MindPulse.Core.Application.DTOs.Evaluations.Analysis;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services.Orchestrations
{
    public interface ITestOrchestrationService
    {
        Task<ApiResponse<TestAnalysisDTO>> AnalyzeAndStoreTestAsync(TestResponseDTO input);
    }
}
