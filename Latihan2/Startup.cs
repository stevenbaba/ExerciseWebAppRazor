using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Latihan2.Entities;
using Latihan2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Latihan2
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
            services.AddMvc();
            
            services.AddDbContextPool<StockDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("LatihanDb");
                options.UseSqlServer(connectionString);

                options.ConfigureWarnings(warnings =>
                {
                    warnings.Throw(RelationalEventId.QueryClientEvaluationWarning);
                });
            });


            services.AddScoped<LoginService>();

            services.AddAuthentication(LoginService.CookieAuthenticationScheme).AddCookie(LoginService.CookieAuthenticationScheme, options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/denied";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
