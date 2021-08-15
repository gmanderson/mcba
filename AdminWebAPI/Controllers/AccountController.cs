using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebAPI.Models;
using AdminWebAPI.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountListController : ControllerBase
    {
        private readonly AccountListManager _repo;

        public AccountListController(AccountListManager repo)
        {
            _repo = repo;
        }

        // GET: api/accountList
        // Returns all accounts from db
        [HttpGet]
        public IEnumerable<AccountList> Get()
        {
            return _repo.GetAll();
        }

        //// GET api/account/5
        /// Returns individual account by ID
        [HttpGet("{id}")]
        public AccountList Get(int id)
        {
            return _repo.Get(id);
        }

        //// POST api/account
        // Adds new account
        [HttpPost]
        public void Post([FromBody] AccountList account)
        {
            _repo.Add(account);
        }

        //// PUT api/account/5
        // Updates acccount entry by ID
        [HttpPut("{id}")]
        public void Put([FromBody] AccountList account)
        {
            _repo.Update(account.AccountNumber, account);
        }

        //// DELETE api/account/5
        /// Deletes account by ID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
