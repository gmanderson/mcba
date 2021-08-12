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
    public class BillPayController : ControllerBase
    {
        private readonly BillPayManager _repo;

        public BillPayController(BillPayManager repo)
        {
            _repo = repo;
        }

        // GET: api/billPay
        // Returns all transactions from db
        [HttpGet]
        public IEnumerable<BillPay> Get()
        {
            return _repo.GetAll();
        }

        //// GET api/billPay/5
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.Get(id);
        }

        //// POST api/billPay
        // Adds new billPay
        [HttpPost]
        public void Post([FromBody] BillPay billPay)
        {
            _repo.Add(billPay);
        }

        //// PUT api/billPay/5
        // Updates billPay entry
        [HttpPut("{id}")]
        public void Put([FromBody] BillPay billPay)
        {
            _repo.Update(billPay.BillPayID, billPay);
        }

        //// DELETE api/billPay/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
