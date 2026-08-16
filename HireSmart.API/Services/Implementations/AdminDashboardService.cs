using HireSmart.API.Data;
using HireSmart.API.DTOs.Dashboard;
using HireSmart.API.Enums;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminDashboardService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AdminDashboardDto> GetDashboardAsync()
        {
            return new AdminDashboardDto
            {
                TotalUsers = await dbContext.Users.CountAsync(),

                TotalRecruiters = await dbContext.Users
                    .CountAsync(u => u.Role == UserRole.Recruiter),

                TotalCandidates = await dbContext.Users
                    .CountAsync(u => u.Role == UserRole.Candidate),

                TotalCompanies = await dbContext.Companies.CountAsync(),

                TotalJobs = await dbContext.Jobs.CountAsync(),

                TotalApplications = await dbContext.Applications.CountAsync(),

                TotalResumes = await dbContext.Resumes.CountAsync()
            };
        }
    }
}