using System;
using AdminWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using AdminWebAPI.Models.Repository;

namespace AdminWebAPI.Models.DataManagers
{
    public class LoginManager : IDataRepository<Login, string>
    {
        private readonly MCBAContext _context;

        public LoginManager(MCBAContext context)
        {
            _context = context;
        }

        public Login Get(string id)
        {
            return _context.Logins.Find(id);
        }

        public IEnumerable<Login> GetAll()
        {
            return _context.Logins.ToList();
        }

        public string Add(Login login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return login.LoginID;
        }

        public string Delete(string id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }

        public string Update(string id, Login login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }
    }
}
