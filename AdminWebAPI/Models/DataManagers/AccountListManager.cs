using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class AccountListManager : IDataRepository<AccountList, int>
    {
        private readonly MCBAContext _context;

        public AccountListManager(MCBAContext context)
        {
            _context = context;
        }

        public AccountList CreateAccountListObject(Account account)
        {
            return new AccountList
            {
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                CustomerID = account.CustomerID,
                Balance = CalculateBalance(account.AccountNumber)

            };
        }

        public AccountList Get(int id)
        {
            return CreateAccountListObject(_context.Accounts.Find(id));
        }

        public IEnumerable<AccountList> GetAll()
        {
            var accounts = _context.Accounts.ToList();
            var accountsList = new List<AccountList>();
            foreach(var account in accounts)
            {
                accountsList.Add(CreateAccountListObject(account));
            }

            return accountsList;
        }

        public int Add(AccountList accountList)
        {
            var account = new Account()
            {
                AccountNumber = accountList.AccountNumber,
                AccountType = accountList.AccountType,
                CustomerID = accountList.CustomerID
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return accountList.AccountNumber;
        }

        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, AccountList accountList)
        {
            var account = new Account()
            {
                AccountNumber = accountList.AccountNumber,
                AccountType = accountList.AccountType,
                CustomerID = accountList.CustomerID
            };

            _context.Update(account);
            _context.SaveChanges();

            return id;
        }

        public decimal CalculateBalance(int id)
        {
            decimal balance = 0;
            _context.Transactions.Where(x => x.AccountNumber == id).ToList().ForEach(transaction => {

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

            return balance;
        }
    }
}
