namespace HireSmart.API.DTOs.Resume
{
    public class ResumeResponseDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }

        public Guid UserId { get; set; }
    }
}