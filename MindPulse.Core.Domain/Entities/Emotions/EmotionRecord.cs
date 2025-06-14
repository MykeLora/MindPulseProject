using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Emotions
{
    public class EmotionRecord : BaseEntity
    {
        public string Text { get; set; }
        public string? DetectedEmotion { get; set; }
        public float Confidence { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
