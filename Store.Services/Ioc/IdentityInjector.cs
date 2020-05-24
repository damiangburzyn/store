
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Data.Database;
using Store.Data.EF.Entities;
using Store.Data.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Store.IoC
{
    public class IdentityInjector
    {

        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // DbContexts
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseSqlServer(configuration.GetConnectionString("StoreConnection"), b => b.MigrationsAssembly("Store.Data.EF"))
                //.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                ); 

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}