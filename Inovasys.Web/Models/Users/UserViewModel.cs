using System.ComponentModel.DataAnnotations;

namespace Inovasys.Web.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Maximum length for name is 100 characters!")]
        [Required]
        public required string Name { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public AddressViewModel? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;

        public string? Website { get; set; } = null!;

        public string? Note { get; set; }

        public bool IsActive { get; set; }

        public string? Password { get; set; }
    }
}
