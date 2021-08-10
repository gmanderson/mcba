using System;
using IBCustomerSite.Data;
using Microsoft.AspNetCore.Mvc;

namespace IBCustomerSite.Controllers
{
    public class BillPayController : Controller
    {
        private readonly MCBAContext _context;

        public BillPayController(MCBAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
