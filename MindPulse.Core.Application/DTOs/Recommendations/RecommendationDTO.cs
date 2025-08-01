
namespace MindPulse.Core.Application.DTOs.Recommendations
{
    public class RecommendationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? EducationalContentId { get; set; }

    }
}
