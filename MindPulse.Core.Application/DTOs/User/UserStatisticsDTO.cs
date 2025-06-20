using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.User
{
    public class UserStatisticsDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalTests { get; set; }
        public int TotalRecommendations { get; set; }
        public int TotalEmotionalAnalyses { get; set; }
        public int TotalEmotionalRecords { get; set; }
    }

}
