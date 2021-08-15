using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebsite.Models
{
    public class AccountDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public char AccountType { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }
        public virtual CustomerDto Customer { get; set; }

        public virtual List<TransactionDto> Transactions { get; set; }

        public virtual List<BillPayDto> BillPays { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Balance { get; set; }

        [Display(Name = "Account Type")]
        public string AccountTypeName { get; set; }
    }

}
