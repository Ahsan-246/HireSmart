using HireSmart.API.Data;
using HireSmart.API.DTOs.Resume;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class ResumeService : IResumeService
    {
        private readonly ApplicationDbContext dbContext;

        public ResumeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Upload Resume
        public async Task<ResumeResponseDto> UploadResumeAsync(IFormFile file, Guid userId)
        {
            // Create Uploads folder if it doesn't exist
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate unique filename
            var uniqueFileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(file.FileName);

            // Full file path
            var filePath = Path.Combine(
                uploadsFolder,
                uniqueFileName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save metadata to database
            var resume = new Resume
            {
                FileName = file.FileName,
                FilePath = filePath,
                UserId = userId
            };

            await dbContext.Resumes.AddAsync(resume);

            await dbContext.SaveChangesAsync();

            return new ResumeResponseDto
            {
                Id = resume.Id,
                FileName = resume.FileName,
                FilePath = resume.FilePath,
                UploadedAt = resume.UploadedAt,
                UserId = resume.UserId
            };
        }

        // Get All
        public async Task<List<ResumeResponseDto>> GetAllResumesAsync()
        {
            var resumes = await dbContext.Resumes.ToListAsync();

            return resumes.Select(resume => new ResumeResponseDto
            {
                Id = resume.Id,
                FileName = resume.FileName,
                FilePath = resume.FilePath,
                UploadedAt = resume.UploadedAt,
                UserId = resume.UserId
            }).ToList();
        }

        // Get By Id
        public async Task<ResumeResponseDto?> GetResumeByIdAsync(Guid id)
        {
            var resume = await dbContext.Resumes.FindAsync(id);

            if (resume == null)
            {
                return null;
            }

            return new ResumeResponseDto
            {
                Id = resume.Id,
                FileName = resume.FileName,
                FilePath = resume.FilePath,
                UploadedAt = resume.UploadedAt,
                UserId = resume.UserId
            };
        }

        // Delete Resume
        public async Task<bool> DeleteResumeAsync(Guid id)
        {
            var resume = await dbContext.Resumes.FindAsync(id);

            if (resume == null)
            {
                return false;
            }

            // Delete physical file
            if (File.Exists(resume.FilePath))
            {
                File.Delete(resume.FilePath);
            }

            dbContext.Resumes.Remove(resume);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<(byte[] FileBytes, string FileName)?> DownloadResumeAsync(Guid resumeId)
        {
            var resume = await dbContext.Resumes
                .FirstOrDefaultAsync(r => r.Id == resumeId);

            if (resume == null)
                return null;

            if (!File.Exists(resume.FilePath))
                return null;

            var bytes = await File.ReadAllBytesAsync(resume.FilePath);

            return (bytes, resume.FileName);
        }
    }
}