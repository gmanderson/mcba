using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class Payee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PayeeID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string Address { get; set; }

        [StringLength(40)]
        [Required]
        public string Suburb { get; set; }

        [RegularExpression(@"[A-Z]{2,3}",
            ErrorMessage = "Must be a 2 or 3 lettered Australian State")]
        [StringLength(3, MinimumLength = 2)]
        [Required]
        public string State { get; set; }

        [RegularExpression(@"[0-9]{4}",
            ErrorMessage = "Must be a 4 digit number")]
        [StringLength(4)]
        [Required]
        public string Postcode { get; set; }

        [RegularExpression(@"[(\()]{1}[(0-9)]{2}[(\))]{1}\s[(0-9)]{4}\s[(0-9)]{4}",
            ErrorMessage = "Must be of the format: (0X) XXXX XXXX")]
        [StringLength(14)]
        [Required]
        public string Phone { get; set; }

        public virtual List<BillPay> BillPays { get; set; }
    }
}
