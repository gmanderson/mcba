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
        private readonly int freeTransactionLimit = 4;
        private readonly decimal transferFee = 0.2M;

        public bool HasServiceCharge()
        {
            // Check if transaction is over free limit and service charge should be applied
            int numberOfTransactions = 0;

            foreach (Transaction t in SourceAccount.Transactions)
            {
                if (t.TransactionType == 'W')
                {
                    numberOfTransactions++;
                }
                if (t.TransactionType == 'T' && t.DestinationAccountNumber != null)
                {
                    numberOfTransactions++;
                }
            }

            if (numberOfTransactions >= freeTransactionLimit)
            {
                return true;
            }

            return false;
        }

        public decimal AddTransferFee()
        {
            return transferFee;
        }
    }
}
