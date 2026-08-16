namespace HireSmart.API.DTOs.Application
{
    public class CreateApplicationRequestDto
    {
        public Guid JobId { get; set; }

        public Guid ResumeId { get; set; }
    }
}