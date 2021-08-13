using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebAPI.Models
{
    public class AccountList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public char AccountType { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }

        public decimal Balance { get; set; }
    }

}
