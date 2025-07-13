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

        public AuthService(IUserRepository repo, IPasswordHasher<User> hasher)
        {
            _repo = repo;
            _hasher = hasher;
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
            var user = await _repo.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid email or password.");
            }

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
