using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using IBCustomerSite.Models;
using IBCustomerSite.ViewModels;
using IBCustomerSite.Data;
using X.PagedList;
using Newtonsoft.Json;

namespace IBCustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MCBAContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        private const string SessionKey_Customer = "";


        public HomeController(ILogger<HomeController> logger, MCBAContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET DEPOSIT
        public async Task<IActionResult> Deposit()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(new DepositViewModel
            {
                Accounts = customer.Accounts
            }
                ) ;
        }

        // GET CONFIRMATION
        public IActionResult DepositConfirmation(DepositViewModel viewModel)
        {
            return View(viewModel);
        }

        // POST - DEPOSIT
        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
                return View(viewModel);
            }
            //if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
            //    return View(viewModel);
            //}

            // Note this code could be moved out of the controller, e.g., into the Model.

            viewModel.Account.Transactions.Add(
                new Transaction
                {
                    TransactionType = 'D',
                    Amount = viewModel.Amount,
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = viewModel.Comment
                });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET WITHDRAWAL
        public async Task<IActionResult> Withdrawal()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(new DepositViewModel
            {
                Accounts = customer.Accounts
            }
                );
        }

        // GET CONFIRMATION
        public IActionResult WithdrawalConfirmation(DepositViewModel viewModel)
        {
            return View(viewModel);
        }


        // POST - WITHDRAWAL
        [HttpPost]
        public async Task<IActionResult> Withdrawal(DepositViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
                return View(viewModel);
            }
            if (viewModel.HasAdequateBalance(viewModel.Amount)) // NEED TO INCLUDE SERVICE FEE
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must not exceed available balance.");
                return View(viewModel);
            }

            //if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
            //    return View(viewModel);
            //}

            // Note this code could be moved out of the controller, e.g., into the Model.

            viewModel.Account.Transactions.Add(
                new Transaction
                {
                    TransactionType = 'W',
                    Amount = viewModel.Amount,
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = viewModel.Comment
                });

            if (viewModel.HasServiceCharge())
            {
                viewModel.Account.Transactions.Add(
                new Transaction
                {
                    TransactionType = 'S',
                    Amount = viewModel.AddAtmWithdrawalFee(),
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = viewModel.Comment
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET TRANSFER
        public async Task<IActionResult> Transfer()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(new TransferViewModel
            {
                Accounts = customer.Accounts
            }
                );
        }

        // GET CONFIRMATION
        public IActionResult TransferConfirmation(TransferViewModel viewModel)
        {
            return View(viewModel);
        }


        // POST - TRANSFER
        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel viewModel)
        {
            viewModel.SourceAccount = await _context.Accounts.FindAsync(viewModel.SourceAccountNumber);
            viewModel.DestinationAccount = await _context.Accounts.FindAsync(viewModel.DestinationAccountNumber);

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
                return View(viewModel);
            }
            if (viewModel.HasAdequateBalance(viewModel.Amount)) // NEED TO INCLUDE SERVICE FEE
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must not exceed available balance.");
                return View(viewModel);
            }

            //if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
            //    return View(viewModel);
            //}

            // Note this code could be moved out of the controller, e.g., into the Model.

            viewModel.SourceAccount.Transactions.Add(
                new Transaction
                {
                    TransactionType = 'T',
                    Amount = viewModel.Amount,
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = viewModel.Comment,
                    DestinationAccountNumber = viewModel.DestinationAccountNumber
                });

            viewModel.DestinationAccount.Transactions.Add(
                new Transaction
                {
                TransactionType = 'T',
                Amount = viewModel.Amount,
                TransactionTimeUtc = DateTime.UtcNow,
                Comment = viewModel.Comment
             });

            if (viewModel.HasServiceCharge())
            {
                viewModel.SourceAccount.Transactions.Add(
                new Transaction
                {
                    TransactionType = 'S',
                    Amount = viewModel.AddTransferFee(),
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = viewModel.Comment
                });
            }


            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        // GET Statement Index
        public async Task<IActionResult> StatementIndex()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var accounts = customer.Accounts;

            return View(accounts);
        }

        // POST Statement Index
        [HttpPost]
        public async Task<IActionResult> StatementIndexToView(int AccountNumber)
        {
            var account = await _context.Accounts.FindAsync(AccountNumber);
            if (account == null)
                return NotFound();


            //// Store a complex object in the session via JSON serialisation.
            var accountJson = JsonConvert.SerializeObject(account, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString(SessionKey_Customer, accountJson);

            return RedirectToAction(nameof(Statement));
        }


        // GET Statement
        public async Task<IActionResult> Statement(int? page = 1)
        {

            var accountJson = HttpContext.Session.GetString(SessionKey_Customer);
            if (accountJson == null)
            {
                return RedirectToAction(nameof(Index)); // OR return BadRequest();
            }

            //// Retrieve complex object from the session via JSON deserialisation.
            var account = JsonConvert.DeserializeObject<Account>(accountJson);

            ViewBag.Account = account;

            // Page the orders, maximum of 4 per page.
            const int pageSize = 4;
            IPagedList<Transaction> pagedList = await _context.Transactions.Where(x => x.AccountNumber == account.AccountNumber).
                OrderByDescending(x => x.TransactionTimeUtc).ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }

        public async Task<IActionResult> Chart()
        {
            // Get transaction total per day for 30 days
            var customer = await _context.Customers.FindAsync(CustomerID);
            List<decimal> dailyTotalsA1 = new List<decimal>();
            List<decimal> dailyTotalsA2 = new List<decimal>();
            List<string> dates = new List<string>();

            for (int i = 0; i<30; i++)
            {
                dailyTotalsA1.Add(customer.Accounts[0].TransactionTotalAtDate(DateTime.Now.Date.AddDays(-i)));
                dates.Add(DateTime.Now.Date.AddDays(-i).ToString("dd MMM"));
                if(customer.Accounts.Count == 2)
                {
                    dailyTotalsA2.Add(customer.Accounts[1].TransactionTotalAtDate(DateTime.Now.Date.AddDays(-i)));
                }
            }


            //List<Transaction> transactions = await _context.Transactions.Where(x => x.AccountNumber == 4100).ToListAsync();

            //List<decimal> amounts = new List<decimal>();
            //foreach (var transaction in transactions)
            //{
            //    amounts.Add(transaction.Amount);
            //}


            // Get balances for pie chart
            //var customer = await _context.Customers.FindAsync(CustomerID);

            List<decimal> accountBalances = new List<decimal>();
            List<string> accountNames = new List<string>();
            foreach (var account in customer.Accounts)
            {
                accountNames.Add(account.AccountTypeName());
                accountBalances.Add(account.CalculateBalance());
            }

            return View(new ChartViewModel
            {
                Dates = dates,
                DailyTotalsA1 = dailyTotalsA1,
                DailyTotalsA2 = dailyTotalsA2,
                AccountBalances = accountBalances,
                AccountNames = accountNames
            });
        }
    }
}
