using System.Collections.Generic;
using IBCustomerSite.Models;
namespace IBCustomerSite.ViewModels
{
    public class DepositViewModel
    {
        public List<Account> Accounts { get; set; }
        public Account Account { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        private readonly int freeTransactionLimit = 4;
        private readonly decimal atmWithdrawalFee = 0.1M;

        public bool HasServiceCharge()
        {
                // Check if transaction is over free limit and service charge should be applied
                int numberOfTransactions = 0;

                foreach (Transaction t in Account.Transactions)
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

        public decimal AddAtmWithdrawalFee()
        {
            return atmWithdrawalFee;
        }
    }

}
