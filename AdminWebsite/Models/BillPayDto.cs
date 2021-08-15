using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebsite.Models
{

    public class BillPayDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "BillPay ID")]
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        [Required]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

        [ForeignKey("Payee")]
        [Required]
        public int PayeeID { get; set; }
        public virtual PayeeDto Payee { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduleTimeUtc { get; set; }

        [Required]
        public char Period { get; set; }

        [Required]
        public bool HasFailed { get; set; }

        [Required]
        [Display(Name = "Block Status")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm:ss}")]
        public bool IsBlocked { get; set; }
    }
}
