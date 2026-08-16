using Microsoft.AspNetCore.Http;

namespace HireSmart.API.DTOs.Resume
{
    public class UploadResumeRequestDto
    {
        public Guid UserId { get; set; }

        public IFormFile File { get; set; } = null!;
    }
}