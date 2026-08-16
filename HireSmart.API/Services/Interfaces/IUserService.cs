using HireSmart.API.DTOs.User;

namespace HireSmart.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsersAsync();

        Task<UserResponseDto?> GetUserByIdAsync(Guid id);

        Task<UserResponseDto?> UpdateUserAsync(Guid id, UpdateUserRequestDto request);

        Task<bool> DeleteUserAsync(Guid id);
    }
}