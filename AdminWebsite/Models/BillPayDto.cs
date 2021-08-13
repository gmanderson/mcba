using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebsite.Models
{

    public class BillPayDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        [Required]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

        [ForeignKey("Payee")]
        [Required]
        public int PayeeID { get; set; }
        public virtual PayeeDto Payee { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ScheduleTimeUtc { get; set; }

        [Required]
        public char Period { get; set; }

        [Required]
        public bool HasFailed { get; set; }
    }
}
