using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tp_nt1.Database;

namespace tp_nt1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigCookie(CookieAuthenticationOptions options)
        {
            options.LoginPath = "/Accesos/Ingresar"; // ruta relativa para login.
            options.AccessDeniedPath = "/Accesos/NoAutorizado"; // ruta relativa para accesos no autorizados por falta de permisos en el rol.
            options.LogoutPath = "/Accesos/Salir"; // ruta relativa para logout
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(ConfigCookie);
            services.AddDbContext<HistoriaClinicaDbContext>(options => options.UseSqlite("filename = clinica.db"));
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
                    pattern: "{controller=Accesos}/{action=Ingresar}/{id?}");
            });

            app.UseCookiePolicy();
        }
    }
}
