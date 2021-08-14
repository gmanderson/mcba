using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebAPI.Models;
using AdminWebAPI.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerLockController : ControllerBase
    {
        private readonly CustomerLockManager _repo;

        public CustomerLockController(CustomerLockManager repo)
        {
            _repo = repo;
        }

        // GET: api/customerLock
        // Returns all customerID, Name, and IsLocked status from db
        [HttpGet]
        public IEnumerable<CustomerLock> Get()
        {
            return _repo.GetAll();
        }

        //// GET api/customerLock/5
        // Returns customerID, Name, and IsLocked status of a single customer by ID
        [HttpGet("{id}")]
        public CustomerLock Get(int id)
        {
            return _repo.Get(id);
        }

        //// PUT api/customerLock/5
        // Updates login entry for IsLocked status by taking in a customerLock object
        [HttpPut("{id}")]
        public void Put([FromBody] CustomerLock customerLock)
        {
            _repo.Update(customerLock.CustomerID, customerLock);
        }
    }
}
