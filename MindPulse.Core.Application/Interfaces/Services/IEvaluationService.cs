using MindPulse.Core.Application.DTOs.Evaluations.Analysis;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Orchestrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IEvaluationService
    {
        Task<TestAnalysisDTO> EvaluateTestAsync(TestResponseDTO submission);
        Task<FreeTextAnalysisDTO> EvaluateFreeTextAsync(FreeTextEvaluationRequest request);
    }
}

