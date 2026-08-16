namespace HireSmart.API.Models.Entities
{
    public class AIEvaluation
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public Application Application { get; set; } = null!;

        public decimal MatchScore { get; set; }

        public string MissingSkills { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;
    }
}