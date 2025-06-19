using MindPulse.Core.Domain.Commons;
using MindPulse.Core.Domain.Entities.Evaluations;

namespace MindPulse.Core.Domain.Entities.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Test> Evaluations { get; set; }
    }
}
