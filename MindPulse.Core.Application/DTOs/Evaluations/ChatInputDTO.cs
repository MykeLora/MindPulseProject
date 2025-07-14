using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class ChatInputDTO
    {
        public int UserId { get; set; }
        public required string Text { get; set; }
    }
}
