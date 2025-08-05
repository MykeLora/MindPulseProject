using MindPulse.Core.Application.DTOs.Emotions;
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
        Task<EmotionAnalysisDTO> EvaluateTestAsync(TestResponseDTO request);
        Task<EmotionAnalysisDTO> EvaluateFreeTextAsync(FreeTextEvaluationRequest request);
    }
}

