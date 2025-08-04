using MindPulse.Core.Domain.Commons;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class TestResult : BaseEntity
    {
    
        public string? Summary { get; set; }
        public float Confidence { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire? Questionnaire { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

    }

}