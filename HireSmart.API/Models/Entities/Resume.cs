namespace HireSmart.API.Models.Entities
{
    public class Resume
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
