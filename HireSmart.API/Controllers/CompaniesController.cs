using HireSmart.API.DTOs.Company;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        // Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequestDto request)
        {
            var response = await companyService.CreateCompanyAsync(request);

            return Ok(response);
        }

        // Get All
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var response = await companyService.GetAllCompaniesAsync();

            return Ok(response);
        }

        // Get By Id
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var response = await companyService.GetCompanyByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Update
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCompany(
            Guid id,
            UpdateCompanyRequestDto request)
        {
            var response = await companyService.UpdateCompanyAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var deleted = await companyService.DeleteCompanyAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}