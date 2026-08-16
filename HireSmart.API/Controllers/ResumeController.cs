using HireSmart.API.DTOs.Resume;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService resumeService;

        public ResumeController(IResumeService resumeService)
        {
            this.resumeService = resumeService;
        }

        // Upload Resume
        [Authorize(Roles = "Candidate")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await resumeService.UploadResumeAsync(
                file,
                userId);

            return Ok(response);
        }

        // Get All Resumes
        [Authorize(Roles = "Candidate")]
        [HttpGet]
        public async Task<IActionResult> GetAllResumes()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await resumeService.GetAllResumesAsync();

            var userResumes = response
                .Where(r => r.UserId == userId)
                .ToList();

            return Ok(userResumes);
        }

        // Get Resume By Id
        [Authorize(Roles = "Candidate")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetResumeById(Guid id)
        {
            var response = await resumeService.GetResumeByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Delete Resume
        [Authorize(Roles = "Candidate")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteResume(Guid id)
        {
            var deleted = await resumeService.DeleteResumeAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("{resumeId}/download")]
        public async Task<IActionResult> DownloadResume(Guid resumeId)
        {
            var result = await resumeService
                .DownloadResumeAsync(resumeId);

            if (result == null)
                return NotFound();

            return File(
                result.Value.FileBytes,
                "application/pdf",
                result.Value.FileName);
        }
    }
}