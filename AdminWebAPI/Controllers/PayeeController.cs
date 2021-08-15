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
    public class PayeeController : ControllerBase
    {
        private readonly PayeeManager _repo;

        public PayeeController(PayeeManager repo)
        {
            _repo = repo;
        }

        // GET: api/payee
        // Returns all payees from db
        [HttpGet]
        public IEnumerable<Payee> Get()
        {
            return _repo.GetAll();
        }

        //// GET api/payee/5
        /// Return individual payee by ID
        [HttpGet("{id}")]
        public Payee Get(int id)
        {
            return _repo.Get(id);
        }

        //// POST api/payee
        // Adds new payee
        [HttpPost]
        public void Post([FromBody] Payee payee)
        {
            _repo.Add(payee);
        }

        //// PUT api/payee/5
        // Updates payee entry by ID
        [HttpPut("{id}")]
        public void Put([FromBody] Payee payee)
        {
            _repo.Update(payee.PayeeID, payee);
        }

        //// DELETE api/payee/5
        /// Deletes payee by ID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
