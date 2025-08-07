using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.TestResults
{
    public class TestResultCreateDTO
    {
        public string Summary { get; set; }
        public float Confidence { get; set; }
        public int QuestionnaireId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
    }
}
