using MindPulse.Core.Domain.Commons;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class AnswerOption : BaseEntity
    {
        public string? Text { get; set; }
        public int Value { get; set; }

        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        

    }
}