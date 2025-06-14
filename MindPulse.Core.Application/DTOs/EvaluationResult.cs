using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs
{
    public class EvaluationResult
    {
        public string Category { get; set; }
        public string Level { get; set; } // Niveles Bajo, Moderado y Alto
        public string Summary { get; set; }
        public string Recommendation { get; set; }
    }
}

