using System;
using System.Linq;
using IBCustomerSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IBCustomerSite.Data;

namespace IBCustomerSite
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MCBAContext(serviceProvider.GetRequiredService<DbContextOptions<MCBAContext>>());

            // Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.

            context.Customers.AddRange(
                new Customer
                {
                    CustomerID = 2100,
                    Name = "Matthew Bolger",
                    Address = "123 Fake Street",
                    Suburb = "Melbourne",
                    Postcode = "3000"
                },
                new Customer
                {
                    CustomerID = 2200,
                    Name = "Rodney Cocker",
                    Address = "456 Real Road",
                    Suburb = "Melbourne",
                    Postcode = "3005"
                },
                new Customer
                {
                    CustomerID = 2300,
                    Name = "Shekhar Kalra"
                });

            context.Logins.AddRange(
                new Login
                {
                    LoginID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2"
                },
                new Login
                {
                    LoginID = "38074569",
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04"
                },
                new Login
                {
                    LoginID = "17963428",
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE"
                });

            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = 'S',
                    CustomerID = 2100
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = 'C',
                    CustomerID = 2100
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = 'S',
                    CustomerID = 2200
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = 'C',
                    CustomerID = 2300
                });

            const string openingBalance = "Opening balance";
            const string format = "dd/MM/yyyy hh:mm:ss tt";
            context.Transactions.AddRange(
                 new Transaction
                 {
                     TransactionType = 'D',
                     AccountNumber = 4100,
                     Amount = 100,
                     Comment = openingBalance,
                     TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null)
                 },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4200,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 500.95m,
                    TransactionTimeUtc = DateTime.ParseExact("15/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("14/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 210m,
                    TransactionTimeUtc = DateTime.ParseExact("13/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 40.55m,
                    TransactionTimeUtc = DateTime.ParseExact("12/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 1000.50m,
                    TransactionTimeUtc = DateTime.ParseExact("11/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 100.00m,
                    TransactionTimeUtc = DateTime.ParseExact("10/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("09/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 200.00m,
                    TransactionTimeUtc = DateTime.ParseExact("08/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 300.50m,
                    TransactionTimeUtc = DateTime.ParseExact("07/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 200.00m,
                    TransactionTimeUtc = DateTime.ParseExact("06/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 650.95m,
                    TransactionTimeUtc = DateTime.ParseExact("05/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("04/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 2210m,
                    TransactionTimeUtc = DateTime.ParseExact("03/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 30.55m,
                    TransactionTimeUtc = DateTime.ParseExact("02/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 1000.50m,
                    TransactionTimeUtc = DateTime.ParseExact("01/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 100.00m,
                    TransactionTimeUtc = DateTime.ParseExact("31/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 50.00m,
                    TransactionTimeUtc = DateTime.ParseExact("30/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 200.00m,
                    TransactionTimeUtc = DateTime.ParseExact("29/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 340.50m,
                    TransactionTimeUtc = DateTime.ParseExact("28/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 250.00m,
                    TransactionTimeUtc = DateTime.ParseExact("26/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 700.00m,
                    TransactionTimeUtc = DateTime.ParseExact("25/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("24/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4100,
                    Amount = 70.50m,
                    TransactionTimeUtc = DateTime.ParseExact("23/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4100,
                    Amount = 25.00m,
                    TransactionTimeUtc = DateTime.ParseExact("22/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 550.95m,
                    TransactionTimeUtc = DateTime.ParseExact("15/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 400.00m,
                    TransactionTimeUtc = DateTime.ParseExact("14/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 2010m,
                    TransactionTimeUtc = DateTime.ParseExact("13/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 45.55m,
                    TransactionTimeUtc = DateTime.ParseExact("12/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 100.50m,
                    TransactionTimeUtc = DateTime.ParseExact("11/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 150.00m,
                    TransactionTimeUtc = DateTime.ParseExact("10/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("09/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 2000.00m,
                    TransactionTimeUtc = DateTime.ParseExact("08/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 300.50m,
                    TransactionTimeUtc = DateTime.ParseExact("07/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 250.00m,
                    TransactionTimeUtc = DateTime.ParseExact("06/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 550.95m,
                    TransactionTimeUtc = DateTime.ParseExact("05/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 400.00m,
                    TransactionTimeUtc = DateTime.ParseExact("04/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 2010m,
                    TransactionTimeUtc = DateTime.ParseExact("03/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 45.55m,
                    TransactionTimeUtc = DateTime.ParseExact("02/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 100.50m,
                    TransactionTimeUtc = DateTime.ParseExact("01/08/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 150.00m,
                    TransactionTimeUtc = DateTime.ParseExact("31/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("30/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 2000.00m,
                    TransactionTimeUtc = DateTime.ParseExact("29/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 300.50m,
                    TransactionTimeUtc = DateTime.ParseExact("28/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 250.00m,
                    TransactionTimeUtc = DateTime.ParseExact("26/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 500.00m,
                    TransactionTimeUtc = DateTime.ParseExact("25/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 2000.00m,
                    TransactionTimeUtc = DateTime.ParseExact("24/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'W',
                    AccountNumber = 4101,
                    Amount = 300.50m,
                    TransactionTimeUtc = DateTime.ParseExact("23/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4101,
                    Amount = 250.00m,
                    TransactionTimeUtc = DateTime.ParseExact("22/07/2021 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = "Opening balance",
                    TransactionTimeUtc = DateTime.ParseExact("05/12/2019 10:00:00 PM", format, null)
                });

            context.SaveChanges();
        }
    }
}
