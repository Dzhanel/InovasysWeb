using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Inovasys.Data.Entites
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(100)]

        public required string Username { get; set; }

        [StringLength(200)]
        public required string Password { get; set; }

        [StringLength(200)]

        public required string Email { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public Address? Address { get; set; }

    }
}
