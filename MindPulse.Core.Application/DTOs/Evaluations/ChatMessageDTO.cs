using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class ChatMessageDTO
    {
        public string Sender { get; set; } // "User" or "AI"
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
