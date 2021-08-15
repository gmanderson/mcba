using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class TransactionManager : IDataRepository<TransactionDto, int>
    {
        private readonly MCBAContext _context;

        public TransactionManager(MCBAContext context)
        {
            _context = context;
        }

        public TransactionDto Get(int id)
        {
            var transaction = _context.Transactions.Find(id);

            var transactionDto = new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                AccountNumber = transaction.AccountNumber,
                DestinationAccountNumber = transaction.DestinationAccountNumber,
                Amount = transaction.Amount,
                Comment = transaction.Comment,
                TransactionTimeUtc = transaction.TransactionTimeUtc,
                TransactionTypeName = TransactionTypeName(transaction.TransactionType)
            };

            return transactionDto;
        }

        public IEnumerable<TransactionDto> GetAll()
        {
            var transactions = _context.Transactions.ToList();
            var transactionList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                transactionList.Add(new TransactionDto {
                    TransactionId = transaction.TransactionId,
                    TransactionType = transaction.TransactionType,
                    AccountNumber = transaction.AccountNumber,
                    DestinationAccountNumber = transaction.DestinationAccountNumber,
                    Amount = transaction.Amount,
                    Comment = transaction.Comment,
                    TransactionTimeUtc = transaction.TransactionTimeUtc,
                    TransactionTypeName = TransactionTypeName(transaction.TransactionType)
                });
             }
            return transactionList;
        }

        public IEnumerable<TransactionDto> GetAll(int id)
        {
            var transactions = _context.Transactions.Where(x => x.AccountNumber == id).ToList();
            var transactionList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                transactionList.Add(new TransactionDto
                {
                    TransactionId = transaction.TransactionId,
                    TransactionType = transaction.TransactionType,
                    AccountNumber = transaction.AccountNumber,
                    DestinationAccountNumber = transaction.DestinationAccountNumber,
                    Amount = transaction.Amount,
                    Comment = transaction.Comment,
                    TransactionTimeUtc = transaction.TransactionTimeUtc,
                    TransactionTypeName = TransactionTypeName(transaction.TransactionType)
                });
            }

            return transactionList;
        }

        public int Add(TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                TransactionId = transactionDto.TransactionId,
                TransactionType = transactionDto.TransactionType,
                AccountNumber = transactionDto.AccountNumber,
                DestinationAccountNumber = transactionDto.DestinationAccountNumber,
                Amount = transactionDto.Amount,
                Comment = transactionDto.Comment,
                TransactionTimeUtc = transactionDto.TransactionTimeUtc
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionId;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                TransactionId = transactionDto.TransactionId,
                TransactionType = transactionDto.TransactionType,
                AccountNumber = transactionDto.AccountNumber,
                DestinationAccountNumber = transactionDto.DestinationAccountNumber,
                Amount = transactionDto.Amount,
                Comment = transactionDto.Comment,
                TransactionTimeUtc = transactionDto.TransactionTimeUtc
            };

            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }


        public string TransactionTypeName(char transactionType)
        {
            switch (transactionType)
            {
                case 'D':
                    return "Deposit";
                case 'W':
                    return "Withdrawal";
                case 'T':
                    return "Transfer";
                case 'B':
                    return "BillPay";
                default:
                    return "Service Charge";

            }
        }


    }
}
