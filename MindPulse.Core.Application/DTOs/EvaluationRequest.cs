using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs
{
    public class EvaluationRequest
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
    }

    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}

