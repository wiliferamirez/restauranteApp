using Microsoft.AspNetCore.Mvc;
using auth.Services;
using auth.DTOs;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize(Policy ="StaffOnly")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _users;

        public UserController(IUserService users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        private bool IsStaffUser() =>
            User.FindFirstValue("IsStaff")?.Equals("True", System.StringComparison.OrdinalIgnoreCase) == true;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            try
            {
                if (!IsStaffUser())
                {
                    return StatusCode(403, new { message = "User Not Authorized" });
                }
                var users = await _users.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving users: " + ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
        {
            try
            {
                if (!IsStaffUser())
                    return StatusCode(403, new { message = "User Not Authorized" });

                var user = await _users.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the user: " + ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            try
            {
                if (!IsStaffUser())
                {
                    return StatusCode(403, new { message = "User Not Authorized" });
                }
                var updatedUser = await _users.UpdateAsync(id, dto);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Email already exists"))
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the user:" + ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!IsStaffUser())
                {
                    return StatusCode(403, new { message = "User Not Authorized" });
                }
                await _users.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the user:" + ex.Message);
            }
        }
    }
}
