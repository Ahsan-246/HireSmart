using HireSmart.API.DTOs.Dashboard;

namespace HireSmart.API.Services.Interfaces
{
    public interface ICandidateDashboardService
    {
        Task<CandidateDashboardDto> GetDashboardAsync(Guid userId);
    }
}