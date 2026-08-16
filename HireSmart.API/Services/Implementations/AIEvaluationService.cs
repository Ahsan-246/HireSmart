using HireSmart.API.Data;
using HireSmart.API.DTOs.AIEvaluation;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HireSmart.API.Services.Implementation
{
    public class AIEvaluationService : IAIEvaluationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IOllamaService ollamaService;

        public AIEvaluationService(
            ApplicationDbContext dbContext,
            IOllamaService ollamaService)
        {
            this.dbContext = dbContext;
            this.ollamaService = ollamaService;
        }

        // Evaluate Application using Qwen AI
        public async Task<AIEvaluationResponseDto> EvaluateApplicationAsync(
            EvaluateApplicationRequestDto request)
        {
            var application = await dbContext.Applications
                .Include(x => x.Job)
                .Include(x => x.Resume)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.ApplicationId);

            if (application == null)
            {
                throw new Exception("Application not found.");
            }

            if (application.Resume == null)
            {
                throw new Exception(
                    "No resume is attached to this application.");
            }

            if (!File.Exists(application.Resume.FilePath))
            {
                throw new Exception(
                    "Resume file could not be found.");
            }

            // --------------------------------------------------
            // 1. Extract resume text
            // --------------------------------------------------

            string resumeText;

            var extension = Path.GetExtension(
                application.Resume.FilePath)
                .ToLower();

            if (extension == ".pdf")
            {
                resumeText = await ExtractPdfTextAsync(
                    application.Resume.FilePath);
            }
            else
            {
                throw new Exception(
                    "Currently only PDF resumes are supported for AI evaluation.");
            }

            if (string.IsNullOrWhiteSpace(resumeText))
            {
                throw new Exception(
                    "Could not extract text from the resume.");
            }

            // --------------------------------------------------
            // 2. Prepare job information
            // --------------------------------------------------

            var jobTitle =
                application.Job?.Title ?? "Unknown";

            var jobDescription =
                application.Job?.Description ??
                "No description provided.";

            var requiredSkills =
                application.Job?.RequiredSkills ??
                "No required skills specified.";

            // --------------------------------------------------
            // 3. Create AI prompt
            // --------------------------------------------------

            var prompt = $$"""
You are an AI recruitment assistant for HireSmart.

Your task is to evaluate a candidate's resume against a specific
job posting.

JOB TITLE:
{{jobTitle}}

REQUIRED SKILLS:
{{requiredSkills}}

JOB DESCRIPTION:
{{jobDescription}}

CANDIDATE RESUME:
{{resumeText}}

IMPORTANT EVALUATION RULES:

1. REQUIRED SKILLS are the primary basis of the evaluation.

2. Compare every skill listed in REQUIRED SKILLS against the
   candidate's resume.

3. A skill should be considered present only when the resume
   clearly provides evidence of that skill.

4. missingSkills may ONLY contain skills that are explicitly
   listed in REQUIRED SKILLS but are not clearly demonstrated
   in the resume.

5. NEVER invent missing skills.

6. NEVER add a skill to missingSkills simply because it is
   commonly expected for the job title.

7. Do NOT penalize the candidate for skills that are not listed
   in REQUIRED SKILLS.

8. The job description may provide additional context, but it
   must NOT introduce new required skills that are absent from
   REQUIRED SKILLS.

9. Use this scoring approach:

   - Required skills match: 70 points
   - Relevant experience: 20 points
   - Job responsibilities and overall fit: 10 points

10. A candidate who clearly demonstrates almost all required
    skills should normally receive a score between 80 and 100.

11. A candidate who demonstrates most required skills should
    normally receive a score between 60 and 79.

12. A candidate who demonstrates approximately half of the
    required skills should normally receive a score between
    40 and 59.

13. A candidate who demonstrates very few required skills should
    normally receive a score below 40.

14. Do not give an extremely low score merely because the
    candidate lacks optional or commonly expected technologies.

15. Be consistent. Candidates with similar required-skill matches
    should receive similar scores.

16. The score must be between 0 and 100.

17. Recommendation must be based primarily on the match score
    and required-skill coverage.

Return ONLY valid JSON in exactly this format:

{
  "matchScore": 0,
  "missingSkills": "skill1, skill2",
  "summary": "short explanation of the candidate's match",
  "recommendation": "Recommended for Interview"
}

Recommendation must be exactly one of:

"Recommended for Interview"
"Consider"
"Not Recommended"

Return JSON only.
Do not include Markdown.
Do not include ```json.
Do not include any additional text.
""";

            // --------------------------------------------------
            // 4. Ask Qwen through Ollama
            // --------------------------------------------------

            var aiResponse =
                await ollamaService.GenerateResponseAsync(prompt);

            if (string.IsNullOrWhiteSpace(aiResponse))
            {
                throw new Exception(
                    "AI returned an empty response.");
            }

            // --------------------------------------------------
            // 5. Parse AI response
            // --------------------------------------------------

            AIResult? result;

            try
            {
                result = JsonSerializer.Deserialize<AIResult>(
                    aiResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                throw new Exception(
                    "AI returned an invalid response.");
            }

            if (result == null)
            {
                throw new Exception(
                    "AI evaluation could not be processed.");
            }

            // Keep score within valid range
            result.MatchScore = Math.Clamp(
                result.MatchScore,
                0,
                100);

            // --------------------------------------------------
            // 6. Save evaluation
            // --------------------------------------------------

            var evaluation = new AIEvaluation
            {
                ApplicationId = application.Id,
                MatchScore = result.MatchScore,
                MissingSkills = result.MissingSkills ?? "",
                Summary = result.Summary ?? "",
                Recommendation = result.Recommendation ?? ""
            };

            await dbContext.AIEvaluations.AddAsync(evaluation);

            await dbContext.SaveChangesAsync();

            // --------------------------------------------------
            // 7. Return result
            // --------------------------------------------------

            return new AIEvaluationResponseDto
            {
                Id = evaluation.Id,
                ApplicationId = evaluation.ApplicationId,
                MatchScore = evaluation.MatchScore,
                MissingSkills = evaluation.MissingSkills,
                Summary = evaluation.Summary,
                Recommendation = evaluation.Recommendation,
                EvaluatedAt = evaluation.EvaluatedAt
            };
        }

        // ------------------------------------------------------
        // Get All Evaluations
        // ------------------------------------------------------

        public async Task<List<AIEvaluationResponseDto>>
            GetAllEvaluationsAsync()
        {
            var evaluations =
                await dbContext.AIEvaluations.ToListAsync();

            return evaluations.Select(e =>
                new AIEvaluationResponseDto
                {
                    Id = e.Id,
                    ApplicationId = e.ApplicationId,
                    MatchScore = e.MatchScore,
                    MissingSkills = e.MissingSkills,
                    Summary = e.Summary,
                    Recommendation = e.Recommendation,
                    EvaluatedAt = e.EvaluatedAt
                }).ToList();
        }

        // ------------------------------------------------------
        // Get Evaluation By Id
        // ------------------------------------------------------

        public async Task<AIEvaluationResponseDto?>
            GetEvaluationByIdAsync(Guid id)
        {
            var evaluation =
                await dbContext.AIEvaluations.FindAsync(id);

            if (evaluation == null)
            {
                return null;
            }

            return new AIEvaluationResponseDto
            {
                Id = evaluation.Id,
                ApplicationId = evaluation.ApplicationId,
                MatchScore = evaluation.MatchScore,
                MissingSkills = evaluation.MissingSkills,
                Summary = evaluation.Summary,
                Recommendation = evaluation.Recommendation,
                EvaluatedAt = evaluation.EvaluatedAt
            };
        }

        // ------------------------------------------------------
        // Delete Evaluation
        // ------------------------------------------------------

        public async Task<bool> DeleteEvaluationAsync(Guid id)
        {
            var evaluation =
                await dbContext.AIEvaluations.FindAsync(id);

            if (evaluation == null)
            {
                return false;
            }

            dbContext.AIEvaluations.Remove(evaluation);

            await dbContext.SaveChangesAsync();

            return true;
        }

        // ------------------------------------------------------
        // PDF TEXT EXTRACTION
        // ------------------------------------------------------

        private async Task<string> ExtractPdfTextAsync(
            string filePath)
        {
            using var document =
                UglyToad.PdfPig.PdfDocument.Open(filePath);

            var text =
                new System.Text.StringBuilder();

            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
            }

            return text.ToString();
        }

        // ------------------------------------------------------
        // AI RESULT MODEL
        // ------------------------------------------------------

        private class AIResult
        {
            public decimal MatchScore { get; set; }

            public string MissingSkills { get; set; } = "";

            public string Summary { get; set; } = "";

            public string Recommendation { get; set; } = "";
        }
    }
}