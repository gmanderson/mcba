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
        [Display(Name = "Transaction Type")]
        public char TransactionType {get; set;}
        [Display(Name = "Transaction Type")]
        public string TransactionTypeName { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

        [ForeignKey("DestinationAccount")]
        [Display(Name = "Destination Account")]
        public int? DestinationAccountNumber { get; set; }
        public virtual AccountDto DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [StringLength(30)]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm:ss}")]
        public DateTime TransactionTimeUtc { get; set; }
    }
}
