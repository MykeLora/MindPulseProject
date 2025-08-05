using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.Test
{
    public class TestSubmissionDTO
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string QuestionnaireName { get; set; }
        public List<AnswerDetailDTO> Answers { get; set; } = new();
    }

    public class AnswerDetailDTO
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
