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
    public class TransactionController : ControllerBase
    {
        private readonly TransactionManager _repo;

        public TransactionController(TransactionManager repo)
        {
            _repo = repo;
        }

        // GET: api/transaction
        // Returns all transactions from db
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _repo.GetAll();
        }

        //// GET api/transaction/5
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _repo.Get(id);
        }

        //// POST api/transaction
        // Adds new transaction
        [HttpPost]
        public void Post([FromBody] Transaction transaction)
        {
            _repo.Add(transaction);
        }

        //// PUT api/transaction/5
        // Updates transaction entry
        [HttpPut("{id}")]
        public void Put([FromBody] Transaction transaction)
        {
            _repo.Update(transaction.TransactionId, transaction);
        }

        //// DELETE api/transaction/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
