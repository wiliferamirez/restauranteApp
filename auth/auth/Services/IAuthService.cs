using auth.DTOs;

namespace auth.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto dto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
