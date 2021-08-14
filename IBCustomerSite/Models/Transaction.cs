using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBCustomerSite.Models
{
    public record Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public char TransactionType {get; set;}

        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal Amount { get; set; }

        [StringLength(30)]
        public string Comment { get; set; }

        [Required]
        public DateTime TransactionTimeUtc { get; set; }
    }
}
