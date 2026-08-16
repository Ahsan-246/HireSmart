using HireSmart.API.Data;
using HireSmart.API.DTOs.User;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get All Users
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await dbContext.Users.ToListAsync();

            return users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            }).ToList();
        }

        // Get User By Id
        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        // Update User
        public async Task<UserResponseDto?> UpdateUserAsync(Guid id, UpdateUserRequestDto request)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Role = request.Role;

            await dbContext.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        // Delete User
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            dbContext.Users.Remove(user);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}