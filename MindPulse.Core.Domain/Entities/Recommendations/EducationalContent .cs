using MindPulse.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Domain.Entities.Recommendations
{
    public class EducationalContent : BaseEntity
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
