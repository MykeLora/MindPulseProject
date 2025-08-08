using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Confidence
{
    public class ConfidenceDailyDTO
    {
        public float AverageConfidence { get; set; }
        public int TotalEntries { get; set; }
        public DateTime Date { get; set; }

    }
}
