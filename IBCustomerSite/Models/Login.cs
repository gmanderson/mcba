using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBCustomerSite.Models
{
    public record Login
    {
        [Column(TypeName = "nchar")]
        [StringLength(8, MinimumLength = 8)]
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; init; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; init; }
        public virtual Customer Customer { get; init; }

        [Column(TypeName = "nchar")]
        [StringLength(64)]
        [Required]
        public string PasswordHash { get; init; }

        [Required]
        public bool IsLocked { get; init; }
    }
}
