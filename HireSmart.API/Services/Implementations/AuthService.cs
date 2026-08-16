using HireSmart.API.Data;
using HireSmart.API.DTOs.Auth;
using HireSmart.API.Helpers;
using HireSmart.API.Models.Entities;
using HireSmart.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HireSmart.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext dbContext;

        private readonly IConfiguration configuration;

        public AuthService(
            ApplicationDbContext dbContext,
            IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return null;
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new LoginResponseDto
            {
                Token = jwtToken,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }


        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // 1. Check if email already exists
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            // 2. Create User object
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                Role = request.Role
            };

            // 3. Save user
            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();

            // 4. Return response
            return new RegisterResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
