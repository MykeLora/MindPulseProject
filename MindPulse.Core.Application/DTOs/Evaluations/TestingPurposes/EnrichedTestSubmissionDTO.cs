using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations.TestingPurposes
{
    public class EnrichedTestSubmissionDTO
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string QuestionnaireName { get; set; }
        public List<AnswerDetailDTO> Answers { get; set; } = new();
    }
}
