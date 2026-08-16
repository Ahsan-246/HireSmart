namespace HireSmart.API.DTOs.Dashboard
{
    public class CandidateDashboardDto
    {
        public int TotalApplications { get; set; }

        public int TotalResumes { get; set; }

        public int PendingApplications { get; set; }

        public int ShortlistedApplications { get; set; }

        public int RejectedApplications { get; set; }
    }
}