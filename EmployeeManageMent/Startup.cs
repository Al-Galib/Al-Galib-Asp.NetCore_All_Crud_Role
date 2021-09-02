using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManageMent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManageMent
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(op => op.UseSqlServer(this.config.GetConnectionString("EDb")));
            services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 8;
                op.Password.RequiredUniqueChars = 3;
                op.Password.RequireNonAlphanumeric = false;
            }
            )
                .AddEntityFrameworkStores<AppDbContext>();
            //oprer ta or niser ta jakono ekta dilay hobe
            //services.Configure<IdentityOptions>(op =>
            //{
            //    op.Password.RequiredLength = 8;
            //    op.Password.RequiredUniqueChars = 3;
            //    op.Password.RequireNonAlphanumeric = false;
            //});
            services.AddMvc(
                options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                        .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
            ).AddXmlSerializerFormatters();

            services.AddAuthentication()
                .AddGoogle(op =>
                {
                    op.ClientId = "1054091144851-rjmnajdg1u44svdhl158v6o6ujrhckmm.apps.googleusercontent.com";
                    op.ClientSecret = "DsawEuD5Vkm5E5C4EIVGDhVN";
                    //op.CallbackPath = "";
                })
                .AddFacebook(op =>
                {
                    op.ClientId = "323633382887123";
                    op.ClientSecret = "3c871597463bdaad3a3be432b2085067";
                });
            services.AddScoped<IEmployeeRefository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
          
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
