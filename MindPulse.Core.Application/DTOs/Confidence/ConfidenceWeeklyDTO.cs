using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Confidence
{
    public class ConfidenceWeeklyDTO
    {
        public float AverageConfidence { get; set; }
        public float AverageEntriesPerDay { get; set; }
        public DateTime StartOfWeek { get; set; }
        public DateTime EndOfWeek { get; set; }

    }
}
