using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taijitan.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Taijitan.Models.Domain;
using Taijitan.Data.Repositories;
using Taijitan.Filters;

namespace Taijitan
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("Teacher", policy => policy.RequireClaim(ClaimTypes.Role, "Teacher"));
                options.AddPolicy("Member", policy => policy.RequireClaim(ClaimTypes.Role, "Member"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<TaijitanDataInitializer>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITrainingDayRepository, TrainingDayRepository>();
            services.AddScoped<IFormulaRepository, FormulaRepository>();
            services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
            services.AddScoped<INonMemberRepository, NonMemberRepository>();
            services.AddScoped<UserFilter>();

            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,TaijitanDataInitializer dataInitializer)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
                app.UseExceptionHandler("/Errors"); //Catcht runtime errors in de code
                app.UseStatusCodePagesWithReExecute("/Errors/Error/{0}"); //Catch errors 404
                app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
            routes.MapRoute(
                name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            dataInitializer.InitializeData().Wait();
        }
    }
}
