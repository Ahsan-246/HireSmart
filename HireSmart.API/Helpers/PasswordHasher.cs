using Microsoft.AspNetCore.Identity;

namespace HireSmart.API.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var hasher = new PasswordHasher<object>();

            return hasher.HashPassword(null!, password);
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            var hasher = new PasswordHasher<object>();

            var result = hasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password);

            return result == PasswordVerificationResult.Success;
        }
    }
}