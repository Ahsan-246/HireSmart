using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService dashboardService;

        public AdminDashboardController(
            IAdminDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await dashboardService.GetDashboardAsync();

            return Ok(dashboard);
        }
    }
}