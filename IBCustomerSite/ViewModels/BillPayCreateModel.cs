using System;
using System.Collections.Generic;
using IBCustomerSite.Models;

namespace IBCustomerSite.ViewModels
{
    public class BillPayCreateModel
    {
        public List<Account> Accounts { get; set; }
        public int AccountNumber { get; set; }
        //public virtual Account Account { get; set; }
        public int PayeeID { get; set; }
        //public virtual Payee Payee { get; set; }
        public List<Payee> Payees { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleTimeUtc { get; set; }
        public char Period { get; set; }

    }

}
