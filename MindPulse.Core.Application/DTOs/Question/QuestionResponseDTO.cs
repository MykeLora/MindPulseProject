using MindPulse.Core.Application.DTOs.AnswerOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Question
{
    public class QuestionResponseDTO
    {
        public int Id { get; set; }
        public int QuestionnaireId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public string? QuestionnaireTitle { get; set; }
        public List<AnswerOptionDTO>? AnswerOptions { get; set; }
    }

}
