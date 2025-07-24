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
        public string Summary { get; set; } // Resumen del análisis emocional
        public string? Recommendation { get; set; } // Recomendación para  tests
        public float Confidence { get; set; } // Puntuación de confianza del 0 al 100%
    }
}

