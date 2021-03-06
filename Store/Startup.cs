using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using NetLearner.SharedLib.Data;
//using NetLearner.SharedLib.Services;
using Microsoft.AspNetCore.Http;
using VueDemoWithAsp.NetCore.VueCoreConnection;
using Store.Data.Database;
using Store.Data.Entities.Identity;
using Store.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Store.Contracts;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GC5.IoC;
using Microsoft.AspNetCore.Antiforgery;
using GC5.Application.Extensions;
using Microsoft.Extensions.Logging;
using Store.Services.Middleware;

namespace Store
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
            services.Configure<Mailing>(Configuration.GetSection("Mailing"));
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.HttpOnly = true;
            });
            IdentityInjector.RegisterServices(services, Configuration);
            services.AddControllers().AddNewtonsoftJson();
            // connect vue app - middleware  
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages();
            AddJWTAuthentication(services);
            services.AddSession();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/#/Login";
                options.AccessDeniedPath = "/#/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.Configure<ConfigurationStorage>(Configuration.GetSection("ConfigurationStorage"));
            ServicesInjector.RegisterServices(services, Configuration);
        }

        private void AddJWTAuthentication(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddHttpContextAccessor();

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, ILoggerFactory loggerFactory)
        {
          
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");               
            }
            else
            {
                app.UseExceptionHandler("/error");     
            }
            app.UseHsts();
            app.UseMiddleware<JWTInHeaderMiddleware>();
            app.UseStaticFiles();
            SingleLogger.Factory = loggerFactory;
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // use middleware and launch server for Vue  
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment())
                {

                    spa.UseVueDevelopmentServer();
                }
            });
            //app.UseHttpMethodOverride();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
