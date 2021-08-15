using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IBCustomerSite.Models;

namespace IBCustomerSite.ViewModels
{

    public class BillPayCreateModel
    {
        public List<Account> Accounts { get; set; }
        public int AccountNumber { get; set; }
        public int PayeeID { get; set; }
        public List<Payee> Payees { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Scheduled Time")]
        public DateTime ScheduleTimeUtc { get; set; }

        public char Period { get; set; }

    }

}
