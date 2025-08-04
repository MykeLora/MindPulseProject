using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.TestResults
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public float Confidence { get; set; }
        public int QuestionnaireId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
