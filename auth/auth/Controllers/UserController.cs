using Microsoft.AspNetCore.Mvc;
using auth.Services;
using auth.DTOs;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _users;

        public UserController(IUserService users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
                       try
            {
                var users = await _users.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }
    }
}
