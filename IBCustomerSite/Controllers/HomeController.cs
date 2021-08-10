﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IBCustomerSite.Models;
using IBCustomerSite.ViewModels;
using IBCustomerSite.Data;

namespace IBCustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MCBAContext _context;
        private readonly int CustomerID = 2100;

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

        // GET DEPOSIT
        public async Task<IActionResult> Deposit2(DepositViewModel viewModel)
        {

            return View(viewModel);
        }

        // GET CONFIRMATION
        public IActionResult Confirmation(DepositViewModel viewModel)
        {
            return View(viewModel);
        }


        // POST - DEPOSIT
        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);

            //if (viewModel.Amount <= 0)
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
            //    return View(viewModel);
            //}
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


        // POST - WITHDRAWAL
        [HttpPost]
        public async Task<IActionResult> Withdrawal(DepositViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);

            //if (account.Transactions.Amount <= 0)
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
            //    return View(viewModel);
            //}
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


        // POST - TRANSFER
        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel viewModel)
        {
            viewModel.SourceAccount = await _context.Accounts.FindAsync(viewModel.SourceAccountNumber);
            viewModel.DestinationAccount = await _context.Accounts.FindAsync(viewModel.DestinationAccountNumber);

            //if (account.Transactions.Amount <= 0)
            //{
            //    ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be positive.");
            //    return View(viewModel);
            //}
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


        // GET ACCOUNT
        public async Task<IActionResult> Statement()
        {
            int accountNumber = 4100;
            var account = await _context.Accounts.FindAsync(accountNumber);
            return View(account);
        }
    }
}
