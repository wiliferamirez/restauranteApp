using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "First name must be between 5 and 25 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Last name must be between 5 and 25 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must have at least 8 characters.")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "Mobile phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^(09\d{7}|\+5939\d{7})$", ErrorMessage = "Phone must be local (09xxxxxxx) or international (+5939xxxxxxx) format.")]
        public string MobilePhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public bool IsStaff { get; set; } = false;
        public bool EmailConfirmed { get; set; } = false;
    }
}
