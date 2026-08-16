using HireSmart.API.DTOs.AIEvaluation;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    [Authorize(Roles = "Admin,Recruiter")]
    public class AIEvaluationController : ControllerBase
    {
        private readonly IAIEvaluationService aiEvaluationService;

        public AIEvaluationController(IAIEvaluationService aiEvaluationService)
        {
            this.aiEvaluationService = aiEvaluationService;
        }

        [HttpPost]
        public async Task<IActionResult> EvaluateApplication(
            EvaluateApplicationRequestDto request)
        {
            var response = await aiEvaluationService
                .EvaluateApplicationAsync(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvaluations()
        {
            var response = await aiEvaluationService
                .GetAllEvaluationsAsync();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEvaluationById(Guid id)
        {
            var response = await aiEvaluationService
                .GetEvaluationByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEvaluation(Guid id)
        {
            var deleted = await aiEvaluationService
                .DeleteEvaluationAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}