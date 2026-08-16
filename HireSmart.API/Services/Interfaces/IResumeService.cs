using HireSmart.API.DTOs.Resume;

namespace HireSmart.API.Services.Interfaces
{
    public interface IResumeService
    {
      //  Task<ResumeResponseDto> UploadResumeAsync(UploadResumeRequestDto request);
        Task<ResumeResponseDto> UploadResumeAsync(IFormFile file, Guid userId);

        Task<List<ResumeResponseDto>> GetAllResumesAsync();

        Task<ResumeResponseDto?> GetResumeByIdAsync(Guid id);

        Task<bool> DeleteResumeAsync(Guid id);

        Task<(byte[] FileBytes, string FileName)?> DownloadResumeAsync(Guid resumeId);
    }
}