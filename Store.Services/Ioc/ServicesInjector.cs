using AutoMapper;
using AutoMapper.Configuration;
using GC5.Application.Utils;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using Store.Contracts;
using Store.Contracts.Managers;
using Store.Data;
using Store.Data.Database;
using Store.Data.EF.Repositories.Base;
using Store.Services;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;
using GC5.Application.AutoMapper;
using Store.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Store.Data.EF.Entities;

namespace GC5.IoC
{
    public class ServicesInjector
    {
        public static void RegisterServices(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {

            services.Configure<Mailing>(configuration.GetSection("Mailing"));
            var mediaType =  configuration.GetValue<EStorageType>("ConfigurationStorage:StorageType");


            services.AddResponseCaching();
            services.AddMemoryCache();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<StorageHelper, StorageHelper>();
            services.AddAutoMapper(MapperExpression.MapperDomainProfile(services), Assembly.GetExecutingAssembly());
            // repositories
            services.AddScoped<IRepository, Repository>();

            //util
            services.AddSingleton<ILocalPageData, LocalPageData>();

            if (mediaType == EStorageType.Azure)
            {
                services.AddScoped<IMediaService, AzureMediaService>();
            }
            if (mediaType == EStorageType.HttpServer || mediaType == EStorageType.LocalServerWwwRoot)
            { services.AddScoped<IMediaService, FileSystemMediaService>(); }


            services.AddScoped<ISeedManager, SeedManager>();
            services.AddScoped<IMailingService, MailSenderManager>();

            // Application
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            //EF data services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDeliveryMethodService, DeliveryMethodService>();
            services.AddScoped<ShoppingCartService, ShoppingCartService>();
            // identity
            //services.AddScoped<IApplicationUsersService, ApplicationUsersService>();
            //services.AddScoped<IAcceptancesService, AcceptancesService>();

        }
    }
}