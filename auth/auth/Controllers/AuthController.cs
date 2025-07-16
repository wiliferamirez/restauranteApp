using auth.DTOs;
using auth.Entities;
using auth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth ?? throw new ArgumentNullException(nameof(auth));
        }
        [HttpPost("register")]
        [AllowAnonymous]
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var response = await _auth.LoginAsync(dto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()),
                new Claim(ClaimTypes.Email, response.Email),
                new Claim("IsStaff", response.IsStaff.ToString()),
            };



            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                });
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete(".AspNetCore.Cookies");
            Response.Cookies.Delete("AuthCookie");

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            var isAuthenticated = User?.Identity?.IsAuthenticated ?? false;
            return Ok(new { isAuthenticated });
        }

    }
}
