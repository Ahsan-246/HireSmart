using System.Security.Claims;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Recruiter")]
    public class RecruiterDashboardController : ControllerBase
    {
        private readonly IRecruiterDashboardService dashboardService;

        public RecruiterDashboardController(
            IRecruiterDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var recruiterId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var dashboard =
                await dashboardService.GetDashboardAsync(recruiterId);

            return Ok(dashboard);
        }
    }
}