using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.EntidadesImplementadas;
using AccesoDatos.Data.Inicializador;
using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Modelos.Entidades;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SitioWeb
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

            /*Configuramos el servicio de geolocalización*/

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    List<CultureInfo> supportedCultures = new()
                    {
                        new CultureInfo("en"),
                        new CultureInfo("es"),
                        new CultureInfo("fr"),
                        new CultureInfo("de"),
                        new CultureInfo("ru")
                    };
                    opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                    opt.SupportedCultures = supportedCultures;
                    opt.SupportedUICultures = supportedCultures;
                });

            /*para la administracion de paginas de redireccion de error uno para cuando hay problema de login,
             y otro para cuando se intenta acceder a un recurso que no existe a traves de url*/
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Cliente/Home/RequiereLogin";
                options.AccessDeniedPath = "/Admin/Account/NotAuthorized";
            });

            /*Para trabajar con variables de sesion*/
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true; // consent required
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //services.AddSession(opts =>
            //{
            //    opts.IdleTimeout = TimeSpan.FromMinutes(60);
            //    opts.Cookie.IsEssential = true; // make the session cookie Essential
            //});

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /*En cualquier proyecto siempre es indispensable colocar todo a como esta este metodo*/
            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.User.RequireUniqueEmail = true;

                /*Esta linea siempre al inicio del proyecto ponerla en false, 
                                                      unicamente ponerla en true cuando se haya colocado todo lo correspondiente
                                                      a manejo de confirmacion de correo*/
                cfg.SignIn.RequireConfirmedEmail = true;

                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0; /*cantidad de caracteres especiales*/
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6; /*cantidad minima requerida de caracteres para la clave*/
            })
            .AddEntityFrameworkStores<SolyLunaDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<SolyLunaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<SolyLunaDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddTransient<SeedDb>();  /*CLASE QUE PERMITE PRECARGAR UNOS DATOS EN LA BASE DE DATOS*/

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

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

            /*esta linea es para indicar que cuando existe un error, pues utilice la pagina de error como tal*/
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            /*Localizations*/

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            //var supportedCultures = new[] { "es", "en", "fr" };
            //var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures);

            //app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            /*Siempre colocar UserAuthentication para permitir usar el User.Identity.Name, esto
             porque cuando no se crea el proyecto con identity, no trae esta linea*/
            app.UseAuthentication();
            app.UseAuthorization();

            /*Agregar siempre esta linea para trabajar con variables de sesion*/
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
