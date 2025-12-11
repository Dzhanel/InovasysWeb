using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovaSys.Data.Entites
{
    public class Address
    {
        //public Guid Id { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Street name can't be longer than 100 characters!")]
        public required string Street { get; set; }
        
        [AllowNull]
        [StringLength(100, ErrorMessage = "Suite name can't be longer than 100 characters!")]
        public string Suite { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "City name can't be longer than 100 characters!")]
        public required string City { get; set; }

        [AllowNull]
        [MaxLength(10, ErrorMessage = "Maximum length for zip code is 10 characters!")]
        public string ZipCode { get; set; }

        [Range(-90.0, 90.0, ErrorMessage = "Incorrect latitude")]
        public double Latitude { get; set; }

        [Range(-180.0, 180.0, ErrorMessage = "Incorect longitude")]
        public double Longitude { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
