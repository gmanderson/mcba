using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebsite.Models
{
    public class TransactionDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public char TransactionType {get; set;}

        [Required]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; set; }
        public virtual AccountDto DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal Amount { get; set; }

        [StringLength(30)]
        public string Comment { get; set; }

        [Required]
        public DateTime TransactionTimeUtc { get; set; }
    }
}
