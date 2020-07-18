using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Shop.Application.UsersAdmin;
using Shop.Application;

namespace Shop.UI
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

            services.AddApplicationServices();
            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(
                opt =>
                {
                    opt.CheckConsentNeeded = content => true;
                    opt.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;

                }
                );
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(""));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();
            services.AddSession(opt=> {
                opt.Cookie.Name="cart";
                opt.Cookie.MaxAge = TimeSpan.FromDays(365);



                });
            StripeConfiguration.ApiKey=Configuration.GetSection("Stripe")["SecretKey"];


            services.AddMvc()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    })
    .SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddIdentity
                <IdentityUser,IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;



                }

                ).AddEntityFrameworkStores<ApplicationDbContext>();


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Accounts/Login";
            });

            services.AddMvc().AddRazorPagesOptions(opt => {


                opt.Conventions.AuthorizeFolder("/Admin");
                opt.Conventions.AuthorizePage("/Admin/ConfigureUsers","Admin");

                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthorizationCore(
                
                opt =>
                {

                    opt.AddPolicy("Admin", policy => policy.RequireClaim("Role","Admin"));
                   //  opt.AddPolicy("Manager", policy => policy.RequireClaim("Role","Manager"));
                    opt.AddPolicy("Manager", policy => policy.RequireAssertion(context => context.User.HasClaim("Role", "Manager")||context.User.HasClaim("Role","Admin")));

                }



                ) ;
        }
    
 



                
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
