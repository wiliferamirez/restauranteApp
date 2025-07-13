using auth.DTOs;

namespace auth.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(Guid id);
        Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserRequestDto dto);
        Task DeleteAsync(Guid id);
    }
}
