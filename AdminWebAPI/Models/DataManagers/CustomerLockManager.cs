using System;
using System.Collections.Generic;
using AdminWebAPI.Data;
using AdminWebAPI.Models.Repository;
using System.Linq;

namespace AdminWebAPI.Models.DataManagers
{
    public class CustomerLockManager : IDataRepository<CustomerLock, int>
    {
        private readonly MCBAContext _context;

        public CustomerLockManager(MCBAContext context)
        {
            _context = context;
        }

        public int Add(CustomerLock item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerLock Get(int id)
        {
            var customer = _context.Customers.Find(id);
            var login = _context.Logins.Where(x => x.CustomerID == customer.CustomerID).First();

            var customerLock = new CustomerLock
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                IsLocked = login.IsLocked
            };

            return customerLock;
        }

        public IEnumerable<CustomerLock> GetAll()
        {
            var customers = _context.Customers.ToList();
            var logins = _context.Logins.ToList();

            var customerLocks = new List<CustomerLock>() { };

            foreach (var customer in customers)
            {
                customerLocks.Add(new CustomerLock
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name,
                    IsLocked = logins.First(x => x.CustomerID == customer.CustomerID).IsLocked
                });
            }

            return customerLocks;
        }

        public int Update(int id, CustomerLock customerLock)
        {
            var login = _context.Logins.First(x => x.CustomerID == id);

            login.IsLocked = customerLock.IsLocked;

            _context.Update(login);
            _context.SaveChanges();

            return id;
        }
    }
}
