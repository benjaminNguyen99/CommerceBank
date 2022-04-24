using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceBank.Interfaces;
using CommerceBank.Repositories;
using Microsoft.EntityFrameworkCore;
using CommerceBank.Models;
using CommerceBank.Datas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using CommerceBank.Services;

namespace CommerceBank
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
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.ClientId = Configuration["App:FacebookClientId"];
                    options.ClientSecret = Configuration["App:FacebookClientSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["App:GoogleClientId"];
                    options.ClientSecret = Configuration["App:GoogleClientSecret"];
                });
               
            services.AddControllersWithViews();
            services.AddScoped<ITransaction, TransactionRepositories>(); /*In case if wish to provide a new implementation to the unit interface
                                                                           we just need to replace the new repository class which also has the
                                                                           implementation for the ITransaction interface (Benjamin Nguyen) */

            services.AddDbContext<TransactionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbconn")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TransactionContext>();
            services.AddScoped<INotification, NotificationRepositories>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailConfirmCode>(Configuration);
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); //copied from "commerce bank"
            });
        }
    }
}
