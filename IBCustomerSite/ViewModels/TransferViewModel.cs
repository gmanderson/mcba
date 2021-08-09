using System.Collections.Generic;
using IBCustomerSite.Models;
namespace IBCustomerSite.ViewModels
{
    public class TransferViewModel
    {
        public List<Account> Accounts { get; set; }
        public Account SourceAccount { get; set; }
        public Account DestinationAccount { get; set; }
        public int SourceAccountNumber { get; set; }
        public int DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

    }
}
