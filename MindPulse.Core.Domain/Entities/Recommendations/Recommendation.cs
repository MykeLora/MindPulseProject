using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities.Categories;

namespace MindPulse.Core.Domain.Entities.Recommendations
{
    public class Recommendation : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int? EducationalContentId { get; set; }
        public EducationalContent? EducationalContent { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
