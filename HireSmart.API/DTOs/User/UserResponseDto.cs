using HireSmart.API.Enums;

namespace HireSmart.API.DTOs.User
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}