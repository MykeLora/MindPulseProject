using MindPulse.Core.Domain.Commons;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class Questionnaire : BaseEntity
    {
        public string? Title { get; set; }
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}