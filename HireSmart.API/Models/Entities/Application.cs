using HireSmart.API.Enums;

namespace HireSmart.API.Models.Entities
{
    public class Application
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public Job Job { get; set; } = null!;

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public Guid ResumeId { get; set; }

        public Resume Resume { get; set; } = null!;

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    }
}