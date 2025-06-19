using MindPulse.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IEvaluationService
    {
        Task<EvaluationResult> EvaluateTestAsync(EvaluationRequest request);
        Task<EvaluationResult> EvaluateFreeTextAsync(FreeTextEvaluationRequest request);
    }
}

