namespace auth.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public DateTime? LastLogin { get; init; }
        public string? MobilePhoneNumber { get; init; }
        public bool IsStaff { get; init; }
        public bool EmailConfirmed { get; init; }
    }
}
