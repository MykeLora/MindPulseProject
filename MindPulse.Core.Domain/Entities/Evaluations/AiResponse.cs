
using MindPulse.Core.Domain.Commons;


namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class AiResponse : BaseEntity
    {
        public string ChatResponse { get; set; }


        // Relationship with Users
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
