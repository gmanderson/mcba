using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class TransactionManager : IDataRepository<Transaction, int>
    {
        private readonly MCBAContext _context;

        public TransactionManager(MCBAContext context)
        {
            _context = context;
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public int Add(Transaction transaction)
        {
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

        public int Update(int id, Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }
    }
}
