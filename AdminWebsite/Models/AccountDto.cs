using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebAPI.Models
{
    public class AccountDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public char AccountType { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }
        public virtual CustomerDto Customer { get; set; }

        public virtual List<TransactionDto> Transactions { get; set; }

        public virtual List<BillPayDto> BillPays { get; set; }
    }

}
