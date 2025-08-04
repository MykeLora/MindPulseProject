using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.AiResponse
{
    public class AiResponseCreateDTO
    {
        public string ChatResponse { get; set; }
        public int UserId { get; set; }
    }
}
