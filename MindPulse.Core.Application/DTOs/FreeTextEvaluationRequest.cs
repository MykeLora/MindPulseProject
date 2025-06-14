using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs
{
    public class FreeTextEvaluationRequest
    {
        public Guid UserId { get; set; }
        public List<string> Messages { get; set; } // Para almacener la conversación entera
    }
}