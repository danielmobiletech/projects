using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using JWTAuth.Data;
//using static Microsoft.EntityFrameworkCore.MySqlServerVersion;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using JWTAuth.Migrations;
using Microsoft.AspNetCore.Http;

namespace JWTAuth
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
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 26));
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddControllers();
            services.AddDbContext<UserContext>(opt => opt.UseMySql("", serverVersion));
            services.AddScoped<JwtService>();
           
            services.AddCors(opt => opt.AddPolicy(name:"greatest",constr=>constr.AllowAnyHeader().AllowAnyMethod().WithOrigins("").AllowCredentials()));



            services.Configure<CookiePolicyOptions>(

                opt=> { //opt.CheckConsentNeeded = context => true;
                    opt.MinimumSameSitePolicy = SameSiteMode.None;
                    opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                    opt.Secure = CookieSecurePolicy.Always;

                }

                );
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

            app.UseCors("greatest");
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
