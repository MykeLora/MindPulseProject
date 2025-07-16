using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class AiResponseDTO
    {
        public int Id { get; set; }
        public string ChatResponse { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
