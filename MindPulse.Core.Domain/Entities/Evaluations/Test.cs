using MindPulse.Core.Domain.Commons;
using MindPulse.Core.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Evaluations
{
    public class Test : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
