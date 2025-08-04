using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.AnswerOption
{
    public class AnswerOptionResponseDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public int QuestionId { get; set; }

    }
}
