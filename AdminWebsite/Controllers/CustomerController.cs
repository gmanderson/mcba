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
    }


}
