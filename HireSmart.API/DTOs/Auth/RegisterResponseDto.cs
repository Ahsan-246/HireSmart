using HireSmart.API.Enums;

namespace HireSmart.API.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}