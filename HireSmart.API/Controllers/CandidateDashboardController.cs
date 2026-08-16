using System.Security.Claims;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Candidate")]
    public class CandidateDashboardController : ControllerBase
    {
        private readonly ICandidateDashboardService dashboardService;

        public CandidateDashboardController(
            ICandidateDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var dashboard =
                await dashboardService.GetDashboardAsync(userId);

            return Ok(dashboard);
        }
    }
}