using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MindPulse.Core.Application.DTOs.AnswerOption;

namespace MindPulse.Core.Application.DTOs.Question
{

    public class QuestionCreateDTO
    {
        [Required(ErrorMessage = "QuestionnaireId is required.")]
        public int QuestionnaireId { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [MinLength(5, ErrorMessage = "Question text must be at least 5 characters.")]
        [MaxLength(300, ErrorMessage = "Question text cannot exceed 300 characters.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Question type is required.")]
        [MinLength(3, ErrorMessage = "Type must be at least 3 characters.")]
        [MaxLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }

        public List<AnswerOptionCreateDTO>? AnswerOptions { get; set; }
    }

}
