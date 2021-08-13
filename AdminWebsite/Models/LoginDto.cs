using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class LoginDto
    {
        [Column(TypeName = "nchar")]
        [StringLength(8, MinimumLength = 8)]
        [Required]
        public string LoginID { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }
        public virtual CustomerDto Customer { get; set; }

        [Column(TypeName = "nchar")]
        [StringLength(64)]
        [Required]
        public string PasswordHash { get; set; }
    }
}
