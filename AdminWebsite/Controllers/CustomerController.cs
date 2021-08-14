using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminWebsite.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using IBCustomerSite.Filters;

namespace AdminWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // GET Customer/Details/{id}
        [AuthorizeCustomer]
        public async Task<IActionResult> Details(int? id)
        {
            var response = await Client.GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a variable.
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);


            return View(customer);
        }

        // GET Customer/Edit/{id}
        [AuthorizeCustomer]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await Client.GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);

            return View(customer);
        }

        // POST: Customer/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeCustomer]
        public IActionResult Edit(int id, CustomerDto customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/customer/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Details), new { id = id });
            }

            return View(customer);
        }

        // POST: Customer/Lock/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeCustomer]
        public IActionResult Lock(int id, CustomerDto customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            customer.IsLocked = true;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/customerLock/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Details), new { id = id });
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        // POST: Customer/Unlock/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeCustomer]
        public IActionResult Unlock(int id, CustomerDto customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            customer.IsLocked = false;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/customerLock/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }


}
