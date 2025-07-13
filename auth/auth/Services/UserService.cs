using auth.Repositories;
using auth.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                LastLogin = u.LastLogin,
                MobilePhoneNumber = u.MobilePhoneNumber,
                IsStaff = u.IsStaff,
                EmailConfirmed = u.EmailConfirmed
            });
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastLogin = user.LastLogin,
                MobilePhoneNumber = user.MobilePhoneNumber,
                IsStaff = user.IsStaff,
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UpdateUserRequestDto dto)
        {
            var u = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");

            var newEmail = dto.Email?.Trim().ToLowerInvariant();
            if (!string.Equals(u.Email, newEmail, StringComparison.OrdinalIgnoreCase) && await _repo.EmailExistsAsync(newEmail))
            {
                throw new ArgumentException("Email already exists.");
            }

            u.Email = newEmail;
            u.FirstName = dto.FirstName.Trim();
            u.LastName = dto.LastName.Trim();
            u.MobilePhoneNumber = dto.MobilePhoneNumber.Trim();
            u.Address = dto.Address.Trim();
            u.IsStaff = dto.IsStaff;
            u.EmailConfirmed = dto.EmailConfirmed;
            u.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _repo.UpdateAsync(u);

            return new UserResponseDto
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                LastLogin = updatedUser.LastLogin,
                MobilePhoneNumber = updatedUser.MobilePhoneNumber,
                IsStaff = updatedUser.IsStaff,
                EmailConfirmed = updatedUser.EmailConfirmed
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");
            await _repo.DeleteAsync(id);
        }
    }
}
