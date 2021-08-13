using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class CustomerDto
    {
        // CustomerID must be 4 digits
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1000, 9999)]
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        // 9 digits including 2 spaces
        [StringLength(11)]
        [RegularExpression(@"[(0-9)]{3}\s[(0-9)]{3}\s[(0-9)]{3}",
            ErrorMessage = "Must be of the format XXX XXX XXX")]
        public string TFN { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string Suburb { get; set; }

        [RegularExpression(@"[A-Z]{2,3}",
            ErrorMessage = "Must be a 2 or 3 lettered Australian State")]
        [StringLength(3, MinimumLength = 2)]
        public string State { get; set; }

        [RegularExpression(@"[0-9]{4}",
            ErrorMessage = "Must be a 4 digit number")]
        [StringLength(4)]
        public string Postcode { get; set; }

        [RegularExpression(@"[(0-9)]{4}\s[(0-9)]{3}\s[(0-9)]{3}",
            ErrorMessage = "Must be of the format: 04XX XXX XXX")]
        [StringLength(12)]
        public string Mobile { get; set; }

        public virtual List<AccountDto> Accounts { get; set; }
    }
}
