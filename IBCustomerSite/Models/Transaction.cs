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
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public char TransactionType {get; set;}

        [Required]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DestinationAccount")]
        [Display(Name = "Destination Account")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public decimal Amount { get; set; }

        [StringLength(30)]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionTimeUtc { get; set; }



        public string TransactionTypeName()
        {
            switch (TransactionType)
            {
                case 'D':
                    return "Deposit";
                case 'W':
                    return "Withdrawal";
                case 'T':
                    return "Transfer";
                case 'B':
                    return "BillPay";
                default:
                    return "Service Charge";

            }
        }

        public string ReturnLocalTime()
        {
            return TransactionTimeUtc.ToLocalTime().ToString("dd/MM/yyyy");
        }
    }


}
