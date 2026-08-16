using HireSmart.API.DTOs.Dashboard;

namespace HireSmart.API.Services.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardDto> GetDashboardAsync();
    }
}