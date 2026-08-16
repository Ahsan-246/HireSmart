using HireSmart.API.DTOs.Auth;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            try
            {
                var response = await authService.RegisterAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var response = await authService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(response);
        }
    }
}