using HireSmart.API.DTOs.Company;

namespace HireSmart.API.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyResponseDto> CreateCompanyAsync(CreateCompanyRequestDto request);

        Task<List<CompanyResponseDto>> GetAllCompaniesAsync();

        Task<CompanyResponseDto?> GetCompanyByIdAsync(Guid id);

        Task<CompanyResponseDto?> UpdateCompanyAsync(Guid id, UpdateCompanyRequestDto request);

        Task<bool> DeleteCompanyAsync(Guid id);
    }
}