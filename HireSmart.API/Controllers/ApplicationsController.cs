using HireSmart.API.DTOs.Application;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        // Create
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public async Task<IActionResult> Apply(
        CreateApplicationRequestDto request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await applicationService.CreateApplicationAsync(
                request,
                userId);

            return Ok(response);
        }

        // Get All
        [Authorize(Roles = "Candidate")]
        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var response = await applicationService.GetAllApplicationsAsync();

            return Ok(response);
        }

        // Get By Id
        [Authorize(Roles = "Candidate")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetApplicationById(Guid id)
        {
            var response = await applicationService.GetApplicationByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Update Status
        [Authorize(Roles = "Candidate")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateApplication(
            Guid id,
            UpdateApplicationRequestDto request)
        {
            var response = await applicationService.UpdateApplicationAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Delete
        [Authorize(Roles = "Candidate")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var deleted = await applicationService
                .DeleteApplicationAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetJobApplications(Guid jobId)
        {
            var response =
                await applicationService.GetJobApplicationsAsync(jobId);

            return Ok(response);
        }

        [Authorize(Roles = "Candidate")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await applicationService
                .GetMyApplicationsAsync(userId);

            return Ok(response);

        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("{applicationId}/status")]
        public async Task<IActionResult> UpdateStatus(Guid applicationId,UpdateApplicationStatusDto request)
        {
            var updated = await applicationService
                .UpdateApplicationStatusAsync(
                    applicationId,
                    request);

            if (!updated)
                return NotFound();

            return Ok("Application status updated successfully.");
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpGet("{applicationId}/resume")]
        public async Task<IActionResult> DownloadResume(Guid applicationId)
        {
            var result = await applicationService
                .DownloadResumeByApplicationAsync(applicationId);

            if (result == null)
                return NotFound();

            return File(
                result.Value.FileBytes,
                "application/pdf",
                result.Value.FileName);
        }
    }
}