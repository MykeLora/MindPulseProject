using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class TestResponsesDTO
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int QuestionnaireId { get; set; }
        public List<TestResponseItemDTO> Answers { get; set; } = new List<TestResponseItemDTO>();
    }

    public class TestResponseItemDTO
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
