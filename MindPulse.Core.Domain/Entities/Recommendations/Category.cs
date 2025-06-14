using MindPulse.Core.Domain.Commons;

namespace MindPulse.Core.Domain.Entities.Recommendations
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<EducationalContent> EducationalContents { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}