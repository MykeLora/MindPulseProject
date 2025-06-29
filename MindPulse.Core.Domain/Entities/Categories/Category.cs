using MindPulse.Core.Domain.Commons;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Domain.Entities.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Relaciones
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<EducationalContent> EducationalContents { get; set; }
    }
}