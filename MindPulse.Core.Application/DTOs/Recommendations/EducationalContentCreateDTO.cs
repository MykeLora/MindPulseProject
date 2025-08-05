using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.Recommendations
{
    public class EducationalContentCreateDTO
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
    }
}
