using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Confidence
{
    public class ConfidenceMontlyDTO
    {
        public float AverageConfidence { get; set; }
        public float AverageEntriesPerWeek { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

    }
}
