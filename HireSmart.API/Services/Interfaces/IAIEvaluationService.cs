using HireSmart.API.DTOs.AIEvaluation;

namespace HireSmart.API.Services.Interfaces
{
    public interface IAIEvaluationService
    {
        Task<AIEvaluationResponseDto> EvaluateApplicationAsync(
            EvaluateApplicationRequestDto request);

        Task<List<AIEvaluationResponseDto>> GetAllEvaluationsAsync();

        Task<AIEvaluationResponseDto?> GetEvaluationByIdAsync(Guid id);

        Task<bool> DeleteEvaluationAsync(Guid id);
    }
}