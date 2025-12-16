using System.ComponentModel.DataAnnotations;

namespace Inovasys.Web.Models.Users
{
    public class SaveUserViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Maximum name length is 100 characters!")]    
        public required string Name { get; set; }
        
        [StringLength(100, ErrorMessage = "Maximum username length is 100 characters!")]
        public required string Username { get; set; }

        [StringLength(200, ErrorMessage = "Maximum email length is 200 characters!")]
        public required string Email { get; set; }

        [StringLength(30, ErrorMessage = "Maximum phone length is 30 characters!")]
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public AddressViewModel? Address { get; set; }

        public string? Website { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public required string Password { get; set; }
    }
}
