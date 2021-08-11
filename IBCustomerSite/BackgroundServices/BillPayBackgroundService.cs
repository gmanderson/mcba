using System;
using System.Threading;
using System.Threading.Tasks;
using IBCustomerSite.Data;
using IBCustomerSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IBCustomerSite.BackgroundServices
{
    public class BillPayBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BillPayBackgroundService> _logger;

        public BillPayBackgroundService(IServiceProvider services, ILogger<BillPayBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BillPay Background Service is running.");

            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);

                _logger.LogInformation("BillPay Background Service is waiting a minute.");

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        // THIS IS WHERE THE BILLPAYS MUST GET RUN
        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BillPay Background Service is working.");

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MCBAContext>();

            var billpays = await context.BillPays.ToListAsync(cancellationToken);

            foreach (var billpay in billpays)
            {
                if(billpay.Amount > billpay.Account.CalculateBalance())
                {
                    billpay.HasFailed = true;

                    await context.SaveChangesAsync();
                }
                else
                {
                    if ((billpay.ScheduleTimeUtc <= DateTime.UtcNow) && !billpay.HasFailed)
                    {
                        context.Transactions.Add(
                            new Transaction
                            {
                                TransactionType = 'B',
                                AccountNumber = billpay.AccountNumber,
                                Amount = billpay.Amount,
                                TransactionTimeUtc = DateTime.UtcNow

                            });

                        if (billpay.Period == 'M')
                        {
                            billpay.ScheduleTimeUtc = DateTime.UtcNow.AddMonths(1);
                        }

                        if (billpay.Period == 'Q')
                        {
                            billpay.ScheduleTimeUtc = DateTime.UtcNow.AddMonths(3);
                        }

                        if (billpay.Period == 'Y')
                        {
                            billpay.ScheduleTimeUtc = DateTime.UtcNow.AddYears(1);
                        }

                        if (billpay.Period == 'O')
                        {
                            context.BillPays.Remove(billpay);
                        }

                        await context.SaveChangesAsync();
                    }
                }

                
            }

            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("BillPay Background Service work complete.");
        }
    }
}

