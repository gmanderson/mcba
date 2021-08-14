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

namespace AdminWebsite.Controllers
{
    public class BillPayController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private readonly ILogger<AccountController> _logger;

        public BillPayController(ILogger<AccountController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // GET BillPay/Index 
        public async Task<IActionResult> Index()
        {
            // Retrieve customers from API
            var response = await Client.GetAsync("api/billPay");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
            var billPays = JsonConvert.DeserializeObject<List<BillPayDto>>(result);

            return View(billPays);
        }

        // PUT BillPay/Block
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Block(int id, BillPayDto billPay)
        {
            if (id != billPay.BillPayID)
                return NotFound();

            if (billPay.AccountNumber == 4100)
                return NotFound();

            billPay.IsBlocked = true;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/billPay/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // PUT BillPay/Unblock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Unblock(int id, BillPayDto billPay)
        {
            if (id != billPay.BillPayID)
                return NotFound();

            if (billPay.AccountNumber == 4100)
                return NotFound();

            billPay.IsBlocked = false;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/billPay/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET BillPay/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var response = await Client.GetAsync($"api/billPay/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a variable.
            var billPay = JsonConvert.DeserializeObject<BillPayDto>(result);


            return View(billPay);
        }
    }
}
