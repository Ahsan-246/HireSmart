using HireSmart.API.Data;
using HireSmart.API.DTOs.Dashboard;
using HireSmart.API.Enums;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class CandidateDashboardService : ICandidateDashboardService
    {
        private readonly ApplicationDbContext dbContext;

        public CandidateDashboardService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CandidateDashboardDto> GetDashboardAsync(Guid userId)
        {
            var totalApplications =
                await dbContext.Applications
                    .CountAsync(a => a.UserId == userId);

            var totalResumes =
                await dbContext.Resumes
                    .CountAsync(r => r.UserId == userId);

            var pending =
                await dbContext.Applications
                    .CountAsync(a =>
                        a.UserId == userId &&
                        a.Status == ApplicationStatus.Pending);

            var shortlisted =
                await dbContext.Applications
                    .CountAsync(a =>
                        a.UserId == userId &&
                        a.Status == ApplicationStatus.Shortlisted);

            var rejected =
                await dbContext.Applications
                    .CountAsync(a =>
                        a.UserId == userId &&
                        a.Status == ApplicationStatus.Rejected);

            return new CandidateDashboardDto
            {
                TotalApplications = totalApplications,
                TotalResumes = totalResumes,
                PendingApplications = pending,
                ShortlistedApplications = shortlisted,
                RejectedApplications = rejected
            };
        }
    }
}