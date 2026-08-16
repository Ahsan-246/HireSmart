namespace HireSmart.API.DTOs.Job
{
    public class JobSearchDto
    {
        public string? Title { get; set; }

        public string? Location { get; set; }

        public Guid? CompanyId { get; set; }
    }
}