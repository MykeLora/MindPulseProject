using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Confidence
{
    public class ConfidenceGlobalDTO
    {
        public float AverageConfidence { get; set; }
        public float AverageEntriesPerMonth { get; set; }
        public int TotalEntries { get; set; }

    }
}
