using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Question
{
    using MindPulse.Core.Application.DTOs.AnswerOption;
    using System.ComponentModel.DataAnnotations;

    public class QuestionUpdateDTO
    {
        [Required(ErrorMessage = "Question Id is required.")]
        public int Id { get; set; }

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

        public List<AnswerOptionUpdateDTO>? AnswerOptions { get; set; }
    }

}
