using HireSmart.API.DTOs.Dashboard;

namespace HireSmart.API.Services.Interfaces
{
    public interface IRecruiterDashboardService
    {
        Task<RecruiterDashboardDto> GetDashboardAsync(Guid recruiterId);
    }
}