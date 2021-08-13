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
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // GET Account/Index 
        public async Task<IActionResult> Index()
        {
            // Retrieve customers from API
            var response = await Client.GetAsync("api/accountList");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
            var accounts = JsonConvert.DeserializeObject<List<AccountDto>>(result);

            return View(accounts);
        }

        // GET Account/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            var response = await Client.GetAsync($"api/account/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a variable.
            var account = JsonConvert.DeserializeObject<AccountDto>(result);


            return View(account);
        }

        // GET Account/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await Client.GetAsync($"api/account/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<AccountDto>(result);

            return View(account);
        }

        // POST: Account/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AccountDto account)
        {
            if (id != account.AccountNumber)
                return NotFound();

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"api/account/{id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Details), new { id = id });
            }

            return View(account);
        }
    }


}
