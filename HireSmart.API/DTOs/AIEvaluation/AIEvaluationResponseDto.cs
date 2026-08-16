namespace HireSmart.API.DTOs.AIEvaluation
{
    public class AIEvaluationResponseDto
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public decimal MatchScore { get; set; }

        public string MissingSkills { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public DateTime EvaluatedAt { get; set; }
    }
}