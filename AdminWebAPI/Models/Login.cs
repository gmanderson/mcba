using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class Login
    {
        [Column(TypeName = "nchar")]
        [StringLength(8, MinimumLength = 8)]
        [Required]
        public string LoginID { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        // MUST STORE SALTED AND HASHED PASSWORD
        [Column(TypeName = "nchar")]
        [StringLength(64)]
        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsLocked { get; set; }
    }
}
