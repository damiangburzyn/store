using AutoMapper;

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
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace GC5.IoC
{
    public class ServicesInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {



            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // repositories
            services.AddScoped<IRepository, Repository>();

          //  services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            //services.AddScoped<IProductsRepository, ProductsRepository>();

            //services.AddScoped<IFileCategoriesRepository, FileCategoriesRepository>();
            //services.AddScoped<IParagraphsRepository, ParagraphsRepository>();




            //util

            services.AddSingleton<ILocalPageData, LocalPageData>();

            //TODO: dorobić odpowiedni warunek na podstawie konfiguracji
            if (false)
            {
                services.AddScoped<IMediaService, AzureMediaService>();
            }
            if (true)
            { services.AddScoped<IMediaService, FileSystemMediaService>(); }


            services.AddScoped<ISeedManager, SeedManager>();
            services.AddScoped<IMailingService, MailSenderManager>();

            // Application
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            //EF data services
            services.AddScoped<ICategoryService, CategoryService>();
         
            
            
            //services.AddScoped<INewsService, NewsService>();
            //services.AddScoped<IPartnerService, PartnerService>();
            //services.AddScoped<IGalleryService, GalleryService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IProductFileService, ProductFileService>();
            //services.AddScoped<ICertificateService, CertificateService>();
            //services.AddScoped<IPropertyService, PropertyService>();
            //services.AddScoped<ITranslationService, TranslationService>();
            //services.AddScoped<IParagraphService, ParagraphService>();
            //services.AddScoped<ITeamMemberService, TeamMemberService>();
            //services.AddScoped<ISolutionService, SolutionService>();
            //services.AddScoped<ICaseStudyService, CaseStudyService>();
            //services.AddScoped<IJobOfferService, JobOfferService>();
            //services.AddScoped<ICooperatorService, CooperatorService>();
            //services.AddScoped<ICompanyService, CompanyService>();
            //services.AddScoped<ITrainingService, TrainingService>();
            //services.AddScoped<IQuestionService, QuestionService>();
            //services.AddScoped<ITrainingTypeService, TrainingTypeService>();
            //services.AddScoped<IPrivacyPolicyService, PrivacyPolicyService>();
            //services.AddScoped<IBannerService, BannerService>();
            //services.AddScoped<IFileCategoryService, FileCategoryService>();
            //services.AddScoped<ITrainingParticipantService, TrainingParticipantService>();
            //services.AddScoped<ITipService, TipService>();
            //services.AddScoped<IAcceptanceTypeService, AcceptanceTypeService>();
            //services.AddSingleton<IWebApiService, WebApiService>();
            //services.AddScoped<IMailchimpService, MailchimpService>();
            //services.AddScoped<IPageService, PageService>();
            //services.AddScoped<IMenuService, MenuService>();
            //services.AddScoped<ISharepointLinkService, SharepointLinkService>();
            //services.AddScoped<IInvestmentService, InvestmentService>();

            //services.AddScoped<ILinkedInService, LinkedInService>();


            // identity
            //services.AddScoped<IApplicationUsersService, ApplicationUsersService>();
            //services.AddScoped<IAcceptancesService, AcceptancesService>();
          
        }
    }
}