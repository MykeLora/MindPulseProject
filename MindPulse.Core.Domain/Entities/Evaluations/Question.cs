using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class Question : BaseEntity
    {
        public int QuestionnaireId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public Questionnaire Questionnaire { get; set; }
        public ICollection<AnswerOption> AnswerOptions { get; set; }
    }
}
