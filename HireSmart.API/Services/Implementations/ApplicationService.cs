using HireSmart.API.Data;
using HireSmart.API.DTOs.Application;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services.Implementations

{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext dbContext;

        public ApplicationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create Application
        public async Task<ApplicationResponseDto> CreateApplicationAsync(CreateApplicationRequestDto request, Guid userId)
        {
            var application = new Application
            {
                JobId = request.JobId,
                ResumeId = request.ResumeId,
                UserId = userId
            };

            await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.Id,
                JobId = application.JobId,
                UserId = application.UserId,
                ResumeId = application.ResumeId,
                AppliedAt = application.AppliedAt,
                Status = application.Status
            };
        }

        // Get All Applications
        public async Task<List<ApplicationResponseDto>> GetAllApplicationsAsync()
        {
            var applications = await dbContext.Applications.ToListAsync();

            return applications.Select(application => new ApplicationResponseDto
            {
                Id = application.Id,
                JobId = application.JobId,
                UserId = application.UserId,
                ResumeId = application.ResumeId,
                AppliedAt = application.AppliedAt,
                Status = application.Status
            }).ToList();
        }

        // Get Application By Id
        public async Task<ApplicationResponseDto?> GetApplicationByIdAsync(Guid id)
        {
            var application = await dbContext.Applications.FindAsync(id);

            if (application == null)
            {
                return null;
            }

            return new ApplicationResponseDto
            {
                Id = application.Id,
                JobId = application.JobId,
                UserId = application.UserId,
                ResumeId = application.ResumeId,
                AppliedAt = application.AppliedAt,
                Status = application.Status
            };
        }

        // Update Application
        public async Task<ApplicationResponseDto?> UpdateApplicationAsync(Guid id, UpdateApplicationRequestDto request)
        {
            var application = await dbContext.Applications.FindAsync(id);

            if (application == null)
            {
                return null;
            }

            application.Status = request.Status;

            await dbContext.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.Id,
                JobId = application.JobId,
                UserId = application.UserId,
                ResumeId = application.ResumeId,
                AppliedAt = application.AppliedAt,
                Status = application.Status
            };
        }

        // Delete Application
        public async Task<bool> DeleteApplicationAsync(
     Guid id,
     Guid userId)
        {
            var application = await dbContext.Applications
                .FirstOrDefaultAsync(a =>
                    a.Id == id &&
                    a.UserId == userId);

            if (application == null)
            {
                return false;
            }

            dbContext.Applications.Remove(application);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ApplicationDetailsDto>> GetJobApplicationsAsync(Guid jobId)
        {
            return await dbContext.Applications
                .Where(a => a.JobId == jobId)
                .Select(a => new ApplicationDetailsDto
                {
                    ApplicationId = a.Id,
                    CandidateName = a.User.FullName,
                    CandidateEmail = a.User.Email,
                    ResumeFile = a.Resume.FileName,
                    Status = a.Status.ToString(),
                    AppliedAt = a.AppliedAt
                })
                .ToListAsync();
        }

        public async Task<List<MyApplicationDto>> GetMyApplicationsAsync(Guid userId)
        {
            return await dbContext.Applications
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.AppliedAt)
                .Select(a => new MyApplicationDto
                {
                    ApplicationId = a.Id,
                    JobTitle = a.Job.Title,
                    CompanyName = a.Job.Company.Name,
                    Status = a.Status.ToString(),
                    AppliedAt = a.AppliedAt
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateApplicationStatusAsync(Guid applicationId, UpdateApplicationStatusDto request)
        {
            var application = await dbContext.Applications
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
                return false;

            application.Status = request.Status;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<(byte[] FileBytes, string FileName)?> DownloadResumeByApplicationAsync(Guid applicationId)
        {
            var application = await dbContext.Applications
                .Include(a => a.Resume)
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
                return null;

            if (!File.Exists(application.Resume.FilePath))
                return null;

            var fileBytes = await File.ReadAllBytesAsync(application.Resume.FilePath);

            return (fileBytes, application.Resume.FileName);
        }
    }
}