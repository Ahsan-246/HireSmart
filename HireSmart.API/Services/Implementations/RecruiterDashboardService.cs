using HireSmart.API.Data;
using HireSmart.API.DTOs.Dashboard;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class RecruiterDashboardService
        : IRecruiterDashboardService
    {
        private readonly ApplicationDbContext dbContext;

        public RecruiterDashboardService(
            ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RecruiterDashboardDto> GetDashboardAsync(
            Guid recruiterId)
        {
            var totalJobs =
                await dbContext.Jobs
                    .CountAsync(j => j.RecruiterId == recruiterId);

            var jobIds =
                await dbContext.Jobs
                    .Where(j => j.RecruiterId == recruiterId)
                    .Select(j => j.Id)
                    .ToListAsync();

            var totalApplications =
                await dbContext.Applications
                    .CountAsync(a => jobIds.Contains(a.JobId));

            var totalCandidates =
                await dbContext.Applications
                    .Where(a => jobIds.Contains(a.JobId))
                    .Select(a => a.UserId)
                    .Distinct()
                    .CountAsync();

            var applicationIds =
                await dbContext.Applications
                    .Where(a => jobIds.Contains(a.JobId))
                    .Select(a => a.Id)
                    .ToListAsync();

            var totalEvaluated =
                await dbContext.AIEvaluations
                    .CountAsync(a => applicationIds.Contains(a.ApplicationId));

            return new RecruiterDashboardDto
            {
                TotalJobs = totalJobs,
                TotalApplications = totalApplications,
                TotalCandidates = totalCandidates,
                TotalEvaluated = totalEvaluated
            };
        }
    }
}