namespace HireSmart.API.DTOs.Job
{
    public class CreateJobRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RequiredSkills { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Location { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
    }
}