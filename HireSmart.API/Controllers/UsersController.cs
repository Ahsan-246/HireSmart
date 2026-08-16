using HireSmart.API.DTOs.User;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await userService.GetAllUsersAsync();

            return Ok(response);
        }

        // Get User By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await userService.GetUserByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Update User
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequestDto request)
        {
            var response = await userService.UpdateUserAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Delete User
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await userService.DeleteUserAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}