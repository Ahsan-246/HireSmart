using HireSmart.API.Data;
using HireSmart.API.DTOs.Company;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext dbContext;

        public CompanyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create Company
        public async Task<CompanyResponseDto> CreateCompanyAsync(CreateCompanyRequestDto request)
        {
            var company = new Company
            {
                Name = request.Name,
                Description = request.Description,
                Website = request.Website,
                Industry = request.Industry
            };

            await dbContext.Companies.AddAsync(company);
            await dbContext.SaveChangesAsync();

            return new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Website = company.Website,
                Industry = company.Industry
            };
        }

        // Get All Companies
        public async Task<List<CompanyResponseDto>> GetAllCompaniesAsync()
        {
            var companies = await dbContext.Companies.ToListAsync();

            return companies.Select(company => new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Website = company.Website,
                Industry = company.Industry
            }).ToList();
        }

        // Get Company By Id
        public async Task<CompanyResponseDto?> GetCompanyByIdAsync(Guid id)
        {
            var company = await dbContext.Companies.FindAsync(id);

            if (company == null)
            {
                return null;
            }

            return new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Website = company.Website,
                Industry = company.Industry
            };
        }

        // Update Company
        public async Task<CompanyResponseDto?> UpdateCompanyAsync(Guid id, UpdateCompanyRequestDto request)
        {
            var company = await dbContext.Companies.FindAsync(id);

            if (company == null)
            {
                return null;
            }

            company.Name = request.Name;
            company.Description = request.Description;
            company.Website = request.Website;
            company.Industry = request.Industry;

            await dbContext.SaveChangesAsync();

            return new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Website = company.Website,
                Industry = company.Industry
            };
        }

        // Delete Company
        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await dbContext.Companies.FindAsync(id);

            if (company == null)
            {
                return false;
            }

            dbContext.Companies.Remove(company);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}