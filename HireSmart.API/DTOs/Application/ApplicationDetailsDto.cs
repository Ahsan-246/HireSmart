namespace HireSmart.API.DTOs.Application
{
    public class ApplicationDetailsDto
    {
        public Guid ApplicationId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public string CandidateEmail { get; set; } = string.Empty;

        public string ResumeFile { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime AppliedAt { get; set; }
    }
}