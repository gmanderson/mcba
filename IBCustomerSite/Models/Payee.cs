using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBCustomerSite.Models
{
    public record Payee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PayeeID { get; init; }

        [StringLength(50)]
        [Required]
        public string Name { get; init; }

        [StringLength(50)]
        [Required]
        public string Address { get; init; }

        [StringLength(40)]
        [Required]
        public string Suburb { get; init; }

        [RegularExpression(@"[A-Z]{2,3}",
            ErrorMessage = "Must be a 2 or 3 lettered Australian State")]
        [StringLength(3, MinimumLength = 2)]
        [Required]
        public string State { get; init; }

        [RegularExpression(@"[0-9]{4}",
            ErrorMessage = "Must be a 4 digit number")]
        [StringLength(4)]
        [Required]
        public string Postcode { get; init; }

        [RegularExpression(@"[(\()]{1}[(0-9)]{2}[(\))]{1}\s[(0-9)]{4}\s[(0-9)]{4}",
            ErrorMessage = "Must be of the format: (0X) XXXX XXXX")]
        [StringLength(14)]
        [Required]
        public string Phone { get; init; }

        public virtual List<BillPay> BillPays { get; init; }
    }
}
