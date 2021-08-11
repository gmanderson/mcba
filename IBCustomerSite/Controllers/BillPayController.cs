using System;
using System.Threading.Tasks;
using IBCustomerSite.Data;
using IBCustomerSite.Models;
using IBCustomerSite.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var accounts = customer.Accounts;

            List<BillPay> billpays = new List<BillPay>();
            foreach(var account in accounts)
            {
                billpays.AddRange(_context.BillPays.Where(x => x.AccountNumber == account.AccountNumber).ToList());
            }
            
            return View(new BillPayViewModel
            {
                BillPays = billpays
            }
                ) ;
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
        public async Task<IActionResult> Create([Bind("Accounts,AccountNumber,PayeeID,Amount,ScheduleTimeUtc,Period")] BillPayCreateModel viewModel)
        {
            //if(viewModel.Payees == null) // NEEDS TO BE FIXED
            //{
            //    return RedirectToAction("Privacy", "Home");
            //}

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
                return View(viewModel);
            }

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
            return View(viewModel);
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

        // GET: Billpay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billpay = await _context.BillPays.FindAsync(id);
            if (billpay == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(CustomerID);
            var payees = await _context.Payees.ToListAsync();

            return View(new BillPayEditViewModel
            {
                Accounts = customer.Accounts,
                AccountNumber = billpay.AccountNumber,
                PayeeID = billpay.PayeeID,
                Payees = payees,
                Amount = billpay.Amount,
                ScheduleTimeUtc = billpay.ScheduleTimeUtc,
                Period = billpay.Period,
                BillPayID = billpay.BillPayID
            });
        }

        // POST: Billpay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Accounts,AccountNumber,PayeeID,Amount,ScheduleTimeUtc,Period,BillPayID")] BillPayEditViewModel viewModel)
        {
            if (id != viewModel.BillPayID)
            {
                return NotFound();
            }

            BillPay billpay = new BillPay
            {
                BillPayID = viewModel.BillPayID,
                AccountNumber = viewModel.AccountNumber,
                PayeeID = viewModel.PayeeID,
                Amount = viewModel.Amount,
                ScheduleTimeUtc = viewModel.ScheduleTimeUtc,
                Period = viewModel.Period
            };

            if (ModelState.IsValid)
            {

                    _context.Update(billpay);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        // GET: BillPay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays
                .FirstOrDefaultAsync(m => m.BillPayID == id);
            if (billPay == null)
            {
                return NotFound();
            }

            return View(billPay);
        }

        // POST: Billpay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billPay = await _context.BillPays.FindAsync(id);
            _context.BillPays.Remove(billPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
