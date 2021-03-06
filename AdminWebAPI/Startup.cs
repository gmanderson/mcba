using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebAPI.Data;
using AdminWebAPI.Models.DataManagers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdminWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<MCBAContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(MCBAContext)));

                //// Enable lazy loading.
                //options.UseLazyLoadingProxies();
            });

            services.AddTransient<MCBAContext>();

            services.AddScoped<LoginManager>();
            services.AddScoped<CustomerManager>();
            services.AddScoped<AccountManager>();
            services.AddScoped<TransactionManager>();
            services.AddScoped<BillPayManager>();
            services.AddScoped<PayeeManager>();
            services.AddScoped<AccountListManager>();
            services.AddScoped<CustomerLockManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
