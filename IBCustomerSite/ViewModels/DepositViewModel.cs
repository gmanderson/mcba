using System.Collections.Generic;
using IBCustomerSite.Models;
namespace IBCustomerSite.ViewModels
{
    public class DepositViewModel
    {
        public List<Account> Accounts { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

    }
}
