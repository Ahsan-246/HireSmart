using HireSmart.API.DTOs.Application;

namespace HireSmart.API.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationResponseDto> CreateApplicationAsync(CreateApplicationRequestDto request,Guid userId);

        Task<List<ApplicationResponseDto>> GetAllApplicationsAsync();

        Task<ApplicationResponseDto?> GetApplicationByIdAsync(Guid id);

        Task<ApplicationResponseDto?> UpdateApplicationAsync(Guid id, UpdateApplicationRequestDto request);

        Task<bool> DeleteApplicationAsync(Guid id, Guid userId);

        Task<List<ApplicationDetailsDto>> GetJobApplicationsAsync(Guid jobId);

        Task<List<MyApplicationDto>> GetMyApplicationsAsync(Guid userId);

        Task<bool> UpdateApplicationStatusAsync( Guid applicationId, UpdateApplicationStatusDto request);

        Task<(byte[] FileBytes, string FileName)?> DownloadResumeByApplicationAsync(Guid applicationId);
    }
}