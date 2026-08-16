using HireSmart.API.DTOs.Auth;

namespace HireSmart.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}