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
        private readonly decimal minimumBalanceChecking = 200M;
        private readonly decimal minimumBalanceSavings = 0M;

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

        // Applies minimum balance by account type
        public decimal MinimumBalanceByAccount()
        {
            switch (Account.AccountType)
            {
                case 'S':
                    return minimumBalanceSavings;
                case 'C':
                    return minimumBalanceChecking;
                default:
                    return 0;
            }
        }

        // Check if balance is adequate
        public bool HasAdequateBalance(decimal totalAmount)
        {
            decimal totalIncludingCharges = totalAmount;

            if (HasServiceCharge())
            {
                totalIncludingCharges += AddAtmWithdrawalFee();
            }
            if (totalIncludingCharges > (Account.CalculateBalance() - MinimumBalanceByAccount()))
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
