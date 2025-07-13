using System.ComponentModel.DataAnnotations;

namespace auth.DTOs
{
    public class UpdateUserRequestDto
    {
        [Required, StringLength(25, MinimumLength = 5)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(25, MinimumLength = 5)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Phone]
        [RegularExpression(@"^(09\d{8}|\+5939\d{8})$", ErrorMessage ="A valid ecuatorian phone number is required.")]
        public string? MobilePhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public bool IsStaff { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
