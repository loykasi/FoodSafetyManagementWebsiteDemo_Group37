using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(12)")]
        public string? CCCD { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string? HoTen { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string? HomeAdress { get; set; }


        // [Required]       
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }


    }
}
