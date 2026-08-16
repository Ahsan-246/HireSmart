using HireSmart.API.DTOs.Job;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobService jobService;

        public JobsController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        [Authorize(Roles = "Admin,Recruiter")]
        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobRequestDto request)
        {
            var recruiterId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await jobService.CreateJobAsync(
                request,
                recruiterId);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var response = await jobService.GetAllJobsAsync();

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var response = await jobService.GetJobByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin,Recruiter")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateJob(
        Guid id,
        UpdateJobRequestDto request)
        {
            var response = await jobService.UpdateJobAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var deleted = await jobService.DeleteJobAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

      //  [AllowAnonymous]
      //  [HttpPost("search")]
        //public async Task<IActionResult> SearchJobs(JobSearchDto request)
        //{
          //  var response = await jobService.SearchJobsAsync(request);

            //return Ok(response);
        //}

        [AllowAnonymous]
        [HttpPost("search")]
        public async Task<IActionResult> SearchJobs([FromBody] JobSearchDto request)
        {
            var response = await jobService.SearchJobsAsync(request);

            return Ok(response);
        }
    }
}
