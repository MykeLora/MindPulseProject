namespace MindPulse.Core.Application.DTOs.Recommendations
{
    public class EducationalContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
