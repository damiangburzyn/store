using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.Contracts.Managers;

namespace Store
{
    public class Program
    {
        private static readonly object Lock = new object();
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            SeedDatabase(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
                 logging.AddEventSourceLogger();
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void SeedDatabase(IHost  host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider; 
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    lock (Lock)
                    {
                        logger.LogDebug("SeedDatabase start");
                        var seedManager = services.GetRequiredService<ISeedManager>();

                        seedManager.Run();
                    }
                }
                catch (Exception ex)
                {
                   

                    logger.LogError(ex, "An error occurred while seeding the database");

                    throw;
                }
            }
        }
    }
}
