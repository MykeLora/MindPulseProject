using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class UserResponse : BaseEntity
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerOptionId { get; set; }
        public string FreeResponse { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public TestResult TestResult { get; set; }
        public Question Question { get; set; }
        public AnswerOption AnswerOption { get; set; }
    }
}
