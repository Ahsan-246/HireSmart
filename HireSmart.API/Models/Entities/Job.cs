namespace HireSmart.API.Models.Entities
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RequiredSkills { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public Guid RecruiterId { get; set; }

        public User Recruiter { get; set; } = null!;
    }
}