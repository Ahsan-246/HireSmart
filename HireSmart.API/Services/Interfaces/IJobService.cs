using HireSmart.API.DTOs.Job;

namespace HireSmart.API.Services.Interfaces
{
    public interface IJobService
    {
        Task<JobResponseDto> CreateJobAsync(CreateJobRequestDto request, Guid recruiterId);

        Task<List<JobResponseDto>> GetAllJobsAsync();

        Task<JobResponseDto?> GetJobByIdAsync(Guid id);

        Task<JobResponseDto?> UpdateJobAsync(Guid id, UpdateJobRequestDto request);

        Task<bool> DeleteJobAsync(Guid id);

        Task<List<JobResponseDto>> SearchJobsAsync(JobSearchDto request);
    }
}