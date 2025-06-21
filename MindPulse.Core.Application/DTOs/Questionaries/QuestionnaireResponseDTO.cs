using MindPulse.Core.Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Questionaries
{
    public class QuestionnaireResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<QuestionResponseDTO> Questions { get; set; }
    }

}
