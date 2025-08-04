using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.AnswerOption
{
    public class AnswerOptionCreateDTO
    {
        [Required(ErrorMessage = "Option text is required.")]
        [MinLength(1, ErrorMessage = "Option text must have at least 1 character.")]
        [MaxLength(100, ErrorMessage = "Option text cannot exceed 100 characters.")]
        public string Text { get; set; }
        public int Value { get; set; }
        public int QuestionId { get; set; }
    }

}
