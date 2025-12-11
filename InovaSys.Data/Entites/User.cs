using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InovaSys.Data.Entites
{
    public class User
    {
        //[Key]
        //public Guid Id { get; set; }

        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Maximum length for name is 100 characters!")]
        public required string Name { get; set; }
        [StringLength(100, ErrorMessage = "Maximum length for username is 100 characters!")]

        public required string Username { get; set; }

        [StringLength(200, ErrorMessage = "Maximum length for password is 200 characters!")]
        public required string Password { get; set; }

        [EmailAddress]
        [StringLength(200, ErrorMessage = "Maximum length for email is 200 characters!")]

        public required string Email { get; set; }

        [AllowNull]
        [StringLength(30, ErrorMessage = "Maximum length for phone is 30 characters!")]
        public string Phone { get; set; }

        [AllowNull]
        public string Website { get; set; }
        [AllowNull]
        public string Note { get; set; }

        public byte IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
