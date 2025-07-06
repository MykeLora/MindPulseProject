using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IOpenAiService
    {
        Task<string> AnalyzeTextAsync(string text);
    }
}
