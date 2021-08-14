using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class BillPayManager : IDataRepository<BillPay, int>
    {
        private readonly MCBAContext _context;

        public BillPayManager(MCBAContext context)
        {
            _context = context;
        }

        public BillPay Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        public IEnumerable<BillPay> GetAll()
        {
            return _context.BillPays.ToList();
        }

        public int Add(BillPay billPay)
        {
            _context.BillPays.Add(billPay);
            _context.SaveChanges();

            return billPay.BillPayID;
        }

        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, BillPay billPay)
        {
            var newBillPay = _context.BillPays.Find(id);

            newBillPay.IsBlocked = billPay.IsBlocked;

            _context.Update(newBillPay);
            _context.SaveChanges();

            return id;
        }
    }
}
