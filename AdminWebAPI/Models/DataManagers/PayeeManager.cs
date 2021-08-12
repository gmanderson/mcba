using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class PayeeManager : IDataRepository<Payee, int>
    {
        private readonly MCBAContext _context;

        public PayeeManager(MCBAContext context)
        {
            _context = context;
        }

        public Payee Get(int id)
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<Payee> GetAll()
        {
            return _context.Payees.ToList();
        }

        public int Add(Payee payee)
        {
            _context.Payees.Add(payee);
            _context.SaveChanges();

            return payee.PayeeID;
        }

        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Payee payee)
        {
            _context.Update(payee);
            _context.SaveChanges();

            return id;
        }
    }
}
