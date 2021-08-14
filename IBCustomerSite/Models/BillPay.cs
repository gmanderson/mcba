using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBCustomerSite.Models
{

    public record BillPay
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("Payee")]
        [Required]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ScheduleTimeUtc { get; set; }

        [Required]
        public char Period { get; set; }

        [Required]
        public bool HasFailed { get; set; }

        public string PeriodNames(char period)
        {
            switch (period)
            {
                case 'M':
                    return "Monthly";
                case 'Q':
                    return "Quarterly";
                case 'Y':
                    return "Yearly";
                case 'O':
                    return "One off";
                default:
                    return "";
            }
        }
    }
}
