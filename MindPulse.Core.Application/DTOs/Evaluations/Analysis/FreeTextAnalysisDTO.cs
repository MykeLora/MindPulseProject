using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.Analysis
{
    public class FreeTextAnalysisDTO
    {
        public string Category { get; set; }
        public string Level { get; set; } // Niveles Bajo, Moderado y Alto
        public string Summary { get; set; } // Resumen del análisis emocional
        public float Confidence { get; set; } // Puntuación de confianza del 0 al 100%
    }
}

