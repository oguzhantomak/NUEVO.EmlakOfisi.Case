using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUEVO.EmlakOfisi.Case.Business.Abstract;
using NUEVO.EmlakOfisi.Case.Business.Concrete;
using NUEVO.EmlakOfisi.Case.Data;
using NUEVO.EmlakOfisi.Case.Data.Abstract;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Data.Concrete.EfCore;
using NUEVO.EmlakOfisi.Case.Entity;

namespace NUEVO.EmlakOfisi.Case.UI
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
            services.AddScoped<IIlanRepository, EfCoreIlanRepository>();
            services.AddScoped<IIlanService, IlanManager>();

            services.AddControllersWithViews();

            // DB işlemleri
            var dataAssemblyName = typeof(EmlakfOfisiContext).Assembly.GetName().Name;
            var x = Configuration.GetConnectionString("Default");
            services.AddDbContext<EmlakfOfisiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly(dataAssemblyName)));

            // Identity işlemleri
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<EmlakfOfisiContext>().AddDefaultTokenProviders();

            // Identity konfigürasyonları, gerekirse diye aşağıda commentli olarak bıraktım.
            services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequireNonAlphanumeric = false;

                    //options.Lockout.MaxFailedAccessAttempts = 5;
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                    options.User.RequireUniqueEmail = true;

                });

            // Cookie işlemleri
            CookieBuilder cookieBuilder = new CookieBuilder();

            cookieBuilder.Name = "EmlakOfisi";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/Logout");
                opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opts.AccessDeniedPath = new PathString("/Member/AccessDenied");
            });

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "detail",
                    pattern: "ilan/detail/{id?}",
                    defaults: new {controller="Ilan", action="Detail"}
                    );

                endpoints.MapControllerRoute(
                    name: "ilanduzenle",
                    pattern: "member/ilanduzenle/{id?}",
                    defaults: new { controller = "Member", action = "IlanDuzenle" }
                );
            });
        }
    }
}
