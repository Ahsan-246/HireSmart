namespace HireSmart.API.DTOs.Job
{
    public class JobResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RequiredSkills { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Location { get; set; } = string.Empty;

        public DateTime PostedDate { get; set; }

        public Guid CompanyId { get; set; }

        public string? CompanyName { get; set; }
    }
}