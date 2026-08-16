using HireSmart.API.Enums;

namespace HireSmart.API.DTOs.User
{
    public class UpdateUserRequestDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}