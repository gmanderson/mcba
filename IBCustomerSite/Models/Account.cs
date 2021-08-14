using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IBCustomerSite.Models
{
    public record Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public char AccountType { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<BillPay> BillPays { get; set; }


        public decimal CalculateBalance()
        {
            decimal balance = 0;
            Transactions.ToList().ForEach(transaction => {

                if (transaction.TransactionType == 'D')
                {
                    balance += transaction.Amount;
                }

                if (transaction.TransactionType == 'W' ||
                    transaction.TransactionType == 'S' ||
                    transaction.TransactionType == 'B')
                {
                    balance -= transaction.Amount;
                }

                if (transaction.TransactionType == 'T')
                {
                    // Incoming transfer
                    if (transaction.DestinationAccountNumber == null)
                    {
                        balance += transaction.Amount;
                    }
                    // Outgoing transfer
                    else
                    {
                        balance -= transaction.Amount;
                    }
                }

            });

            //foreach(Transaction transaction in Transactions)
            //{
            //    balance += transaction.Amount;
            //}

            return balance;
        }

        public decimal TransactionTotalAtDate(DateTime date)
        {
            decimal total = 0;

            Transactions.ToList().ForEach(transaction => {
                if (transaction.TransactionTimeUtc.Date == date.Date)
                {
                    if (transaction.TransactionType == 'D')
                    {
                        total += transaction.Amount;
                    }

                    if (transaction.TransactionType == 'W' ||
                        transaction.TransactionType == 'S' ||
                        transaction.TransactionType == 'B')
                    {
                        total -= transaction.Amount;
                    }

                    if (transaction.TransactionType == 'T')
                    {
                        // Incoming transfer
                        if (transaction.DestinationAccountNumber == null)
                        {
                            total += transaction.Amount;
                        }
                        // Outgoing transfer
                        else
                        {
                            total -= transaction.Amount;
                        }
                    }
                }
                

            });

            return total;
        }

        public string AccountTypeName()
        {
            if (AccountType == 'S')
            {
                return "Savings";
            }
            return "Checking";
        }
    }

}
