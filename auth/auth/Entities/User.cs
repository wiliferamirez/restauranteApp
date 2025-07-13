using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? MobilePhoneNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public bool IsStaff { get; set; } = false;
        public bool EmailConfirmed { get; set; } = false;
    }
}
