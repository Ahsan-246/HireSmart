using HireSmart.API.Data;
using HireSmart.API.DTOs.Job;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext dbContext;

        public JobService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<JobResponseDto> CreateJobAsync(
            CreateJobRequestDto request,
            Guid recruiterId)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                RequiredSkills = request.RequiredSkills,
                Salary = request.Salary,
                Location = request.Location,
                CompanyId = request.CompanyId,
                RecruiterId = recruiterId
            };

            await dbContext.Jobs.AddAsync(job);
            await dbContext.SaveChangesAsync();

            var company = await dbContext.Companies
                .FirstOrDefaultAsync(c => c.Id == job.CompanyId);

            return new JobResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                RequiredSkills = job.RequiredSkills,
                Salary = job.Salary,
                Location = job.Location,
                CompanyId = job.CompanyId,
                CompanyName = company?.Name,
                PostedDate = job.PostedDate
            };
        }

        public async Task<List<JobResponseDto>> GetAllJobsAsync()
        {
            return await dbContext.Jobs
                .Include(job => job.Company)
                .OrderByDescending(job => job.PostedDate)
                .Select(job => new JobResponseDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    RequiredSkills = job.RequiredSkills,
                    Salary = job.Salary,
                    Location = job.Location,
                    CompanyId = job.CompanyId,
                    PostedDate = job.PostedDate,
                    CompanyName = job.Company != null
                        ? job.Company.Name
                        : "Company not available"
                })
                .ToListAsync();
        }

        public async Task<JobResponseDto?> GetJobByIdAsync(Guid id)
        {
            return await dbContext.Jobs
                .Include(j => j.Company)
                .Where(j => j.Id == id)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    RequiredSkills = j.RequiredSkills,
                    Salary = j.Salary,
                    Location = j.Location,
                    CompanyId = j.CompanyId,
                    PostedDate = j.PostedDate,
                    CompanyName = j.Company != null
                        ? j.Company.Name
                        : "Company not available"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<JobResponseDto?> UpdateJobAsync(
            Guid id,
            UpdateJobRequestDto request)
        {
            var job = await dbContext.Jobs
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return null;
            }

            job.Title = request.Title;
            job.Description = request.Description;
            job.RequiredSkills = request.RequiredSkills;
            job.Salary = request.Salary;
            job.Location = request.Location;

            await dbContext.SaveChangesAsync();

            var company = await dbContext.Companies
                .FirstOrDefaultAsync(c => c.Id == job.CompanyId);

            return new JobResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                RequiredSkills = job.RequiredSkills,
                Salary = job.Salary,
                Location = job.Location,
                CompanyId = job.CompanyId,
                CompanyName = company?.Name,
                PostedDate = job.PostedDate
            };
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            var job = await dbContext.Jobs
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return false;
            }

            dbContext.Jobs.Remove(job);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<JobResponseDto>> SearchJobsAsync(
            JobSearchDto request)
        {
            var query = dbContext.Jobs
                .Include(j => j.Company)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                var title = request.Title.Trim();

                query = query.Where(j =>
                    j.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(request.Location))
            {
                var location = request.Location.Trim();

                query = query.Where(j =>
                    j.Location.Contains(location));
            }

            if (request.CompanyId.HasValue)
            {
                query = query.Where(j =>
                    j.CompanyId == request.CompanyId.Value);
            }

            return await query
                .OrderByDescending(j => j.PostedDate)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    RequiredSkills = j.RequiredSkills,
                    Salary = j.Salary,
                    Location = j.Location,
                    CompanyId = j.CompanyId,
                    PostedDate = j.PostedDate,
                    CompanyName = j.Company != null
                        ? j.Company.Name
                        : "Company not available"
                })
                .ToListAsync();
        }
    }
}