using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Emotions
{
    public class EmotionalAnalysis : BaseEntity
    {
        public string InputText { get; set; }
        public string DetectedEmotion { get; set; }
        public float Confidence { get; set; }
        public DateTime AnalysisDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Summary { get; set; }
    }
}
