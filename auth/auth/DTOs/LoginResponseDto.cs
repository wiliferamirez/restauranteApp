namespace auth.DTOs
{
    public class LoginResponseDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
        public DateTime LastLogin { get; init; }
        public bool IsStaff { get; init; }
    }
}
