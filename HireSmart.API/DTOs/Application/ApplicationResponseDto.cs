using HireSmart.API.Enums;

namespace HireSmart.API.DTOs.Application
{
    public class ApplicationResponseDto
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public Guid UserId { get; set; }

        public Guid ResumeId { get; set; }

        public DateTime AppliedAt { get; set; }

        public ApplicationStatus Status { get; set; }
    }
}