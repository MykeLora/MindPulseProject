using MindPulse.Core.Domain.Commons;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class TestResult : BaseEntity
    {
    
        public DateTime CompletionDate { get; set; }
        public string? Summary { get; set; }
        public float SeverityScore { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire? Questionnaire { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

    }

}