using MindPulse.Core.Domain.Commons;
using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Type { get; set; } // Default type is "Standard User"

        public bool IsConfirmed { get; set; } = false;
        public bool IsSuspended { get; set; } = false;
        public int FailedLoginAttempts { get; set; }
        public string? VerificationToken { get; set; }

        public ICollection<EmotionalAnalysis> EmotionalAnalyses { get; set; }
        public ICollection<EmotionalHistory> EmotionalHistories { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<EmotionRecord> EmotionalRecords { get; set; }
        public ICollection<UserResponse> UserResponses { get; set; }
    }

}
