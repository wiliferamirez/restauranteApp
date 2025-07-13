using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.DTOs;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth ?? throw new ArgumentNullException(nameof(auth));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            try
            {
                await _auth.RegisterAsync(dto);
                return Created(String.Empty, "User registered successfully.");
            }
            catch (ArgumentException ex) when (ex.Message.Contains("exists"))
            {
                return Conflict(ex.Message);
            }
        }
    }
}
