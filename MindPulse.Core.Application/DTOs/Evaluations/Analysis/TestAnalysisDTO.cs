using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.Analysis
{
    public class TestAnalysisDTO
    {
        public string Summary { get; set; } // Resumen del análisis del test
        public float Confidence { get; set; } // Puntuación de confianza del 0 al 100%
        public string Recommendation { get; set; } // Recomendación para el usuario
        public EducationalContentSnippetDTO? Resource { get; set; } // Recurso educativo relacionado, si aplica
    }

    public class EducationalContentSnippetDTO
    {
        public string Title { get; set; } // Nombre de la página
        public string Description { get; set; } // Descripción del recurso educativo
        public string Url { get; set; } // URL 
    }
}
