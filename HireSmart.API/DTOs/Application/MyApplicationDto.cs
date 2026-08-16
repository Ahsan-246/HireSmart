namespace HireSmart.API.DTOs.Application
{
    public class MyApplicationDto
    {
        public Guid ApplicationId { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime AppliedAt { get; set; }
    }
}