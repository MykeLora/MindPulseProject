using MindPulse.Core.Application.DTOs;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IFreeTextOrchestrationService
    {
        Task<ApiResponse<List<ChatMessageDTO>>> GetFullChatAsync(int userId);
        Task<string> AnalyzeAndStoreAsync(int userId, string input);
    }
}
