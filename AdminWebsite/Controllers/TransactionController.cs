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
using X.PagedList;
using Microsoft.AspNetCore.Http;
using IBCustomerSite.Filters;

namespace AdminWebsite.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // GET Transaction/Transactions
        [AuthorizeCustomer]
        public async Task<IActionResult> Transactions(decimal? amountLow, decimal? amountHigh)
        {

            // Retrieve customers from API
            var response = await Client.GetAsync("api/transaction");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result).OrderByDescending(x => x.TransactionTimeUtc);

            ViewData["LowFilter"] = amountLow;
            ViewData["HighFilter"] = amountHigh;
            if (amountLow != null && amountHigh == null)
            {
                transactions = transactions.Where(x => (x.Amount >= amountLow)).OrderByDescending(x => x.TransactionTimeUtc);
            }
            if (amountLow == null && amountHigh != null)
            {
                transactions = transactions.Where(x => (x.Amount <= amountHigh)).OrderByDescending(x => x.TransactionTimeUtc);
            }
            if (amountLow != null && amountHigh != null)
            {
                transactions = transactions.Where(x => (x.Amount >= amountLow) && (x.Amount <= amountHigh)).OrderByDescending(x => x.TransactionTimeUtc);
            }


            return View(transactions);
        }
    }
}
