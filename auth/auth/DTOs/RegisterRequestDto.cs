using System.ComponentModel.DataAnnotations;

namespace auth.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must have 8–100 characters.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "First name must be 5–25 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Last name must be 5–25 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Mobile phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^(09\d{7}|\+5939\d{7})$",
            ErrorMessage = "Phone must be 09xxxxxxx or +5939xxxxxxx.")]
        public string MobilePhoneNumber { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }
    }
}
