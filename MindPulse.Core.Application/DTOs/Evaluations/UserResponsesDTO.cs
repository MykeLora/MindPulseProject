using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Evaluations
{
    public class UserResponsesDTO
    {
        public int Id { get; set; }
        public int? TestResultId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerOptionId { get; set; }
        public string? FreeResponse { get; set; }
        public int UserId { get; set; }
    }
}
