using auth.DTOs;
using auth.Entities;
using Microsoft.AspNetCore.Identity;
using auth.Repositories;

namespace auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IPasswordHasher<User> hasher, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _repo = repo;
            _hasher = hasher;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
            {
                throw new ArgumentException("Email already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhoneNumber = dto.MobilePhoneNumber,
                Address = dto.Address,
                PasswordHash = _hasher.HashPassword(null, dto.Password)
            };

            await _repo.CreateAsync(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            // Validate the captcha token

            var client = _httpClientFactory.CreateClient();
            var secret = _config["Captcha:SecretKey"];
            
            var form = new Dictionary<string, string>
            {
                ["secret"] = secret,
                ["response"] = dto.CaptchaToken
            };

            using var content = new FormUrlEncodedContent(form);
            using var verifyResp = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

            if (!verifyResp.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Error calling reCAPTCHA ");
            }

            var payload = await verifyResp.Content.ReadFromJsonAsync<RecaptchaVerifyResponse>();
            if (payload == null || !payload.Success)
            {
                throw new ArgumentException("Captcha verification failed.");
            }

            // Check if the user exists and verify the password

            var user = await _repo.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            // Verify the password using the hasher

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            // Update last login time and return user info

            user.LastLogin = DateTime.UtcNow;
            await _repo.UpdateAsync(user);
            
            return new LoginResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                LastLogin = user.LastLogin!.Value,
                IsStaff = user.IsStaff
            };
        }
    }
}
