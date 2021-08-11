using System;
using System.Threading.Tasks;
using IBCustomerSite.Data;
using IBCustomerSite.Models;
using IBCustomerSite.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IBCustomerSite.Controllers
{
    public class BillPayController : Controller
    {
        private readonly MCBAContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public BillPayController(MCBAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var payees = await _context.Payees.ToListAsync();

            return View(new BillPayCreateModel
            {
                Accounts = customer.Accounts,
                Payees = payees
            }
                );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,PayeeID,Amount,ScheduleTimeUtc,Period")] BillPayCreateModel viewModel)
        {
            BillPay billPay = new BillPay
            {
                AccountNumber = viewModel.AccountNumber,
                PayeeID = viewModel.PayeeID,
                Amount = viewModel.Amount,
                ScheduleTimeUtc = viewModel.ScheduleTimeUtc,
                Period = viewModel.Period
            };

            if (ModelState.IsValid)
            {
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billPay);
        }

        public IActionResult CreatePayee()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayee([Bind("Name,Address,Suburb,State,Postcode,Phone")] Payee viewPayee)
        {
            Payee payee = new Payee
            {
                Name = viewPayee.Name,
                Address = viewPayee.Address,
                Suburb = viewPayee.Suburb,
                State = viewPayee.State,
                Postcode = viewPayee.Postcode,
                Phone = viewPayee.Phone
            };

            if (ModelState.IsValid)
            {
                _context.Add(payee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(payee);
        }
    }
}
