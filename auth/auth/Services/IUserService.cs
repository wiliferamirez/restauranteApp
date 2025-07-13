using auth.DTOs;

namespace auth.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
    }
}
