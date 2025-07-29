using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public DateTime CompletionDate { get; set; }
        public String Summary { get; set; }
        public float SeverityScore { get; set; }
        public int QuestinnaireId { get; set; }
        public int UserId { get; set; }
    }
}
