using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LibraryService;
using LibraryData.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using MySql.Data.EntityFrameworkCore;
namespace Library
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
            services.AddControllersWithViews();
            
            services.AddDbContextPool<LibraryContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("LibraryConnect")));
            //Configuration for dependancy Injection
            services.AddSingleton(Configuration);
            services.AddScoped<ILibraryCardService, LibraryCardService>();
            services.AddScoped<ILibraryBranchService, LibraryBranchService>();
            services.AddScoped<IPatronService, PatronService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<ILibraryAssetService, LibraryAssetService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddControllersWithViews();

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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
