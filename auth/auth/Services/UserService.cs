using auth.Repositories;
using auth.DTOs;

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
    }
}
