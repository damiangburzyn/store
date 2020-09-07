using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Contracts;

namespace Store.Controllers
{
   // [AutoValidateAntiforgeryToken]
    public abstract class BaseController : Controller
    {
        protected readonly AppSettings _appsettings;

        public IMapper Mapper { get; private set; }
     //   internal IWebApiService WebApiService { get; }
        internal ILocalPageData pageData { get; set; }

        protected BaseController(IOptions<AppSettings> applicationSettings,
           // IWebApiService webApiService,
            ILocalPageData pageData,
            IMapper mapper)
        {
            _appsettings = applicationSettings.Value;
            Mapper = mapper;
            this.pageData = pageData;
          //  this.WebApiService = webApiService;
        }

        protected BaseController(IOptions<AppSettings> applicationSettings
            //, IWebApiService webApiService
            )
        {
            _appsettings = applicationSettings.Value;
            //this.WebApiService = webApiService;
        }




        ///// <summary>
        /////     Set culture before action in controller execute
        ///// </summary>
        ///// <param name="context"></param>
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //        // Chack if action need to validate url
        //    var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        //    if (descriptor.MethodInfo.GetCustomAttributes().Any(x => x.GetType() == typeof(ValidateUrlAttribute)))
        //    {
        //        var redirectResponse = ApplicationUrlManager.GetRedirectResponseSettings(Url, Request, RouteData, ConfigurationKeys);
        //        if (redirectResponse.AllowToPerform)
        //        {
        //            // Redirect to same page but with correct url
        //            context.Result = new RedirectResult(redirectResponse.Url, redirectResponse.IsPermanent);
        //            return;
        //        }
        //    }
        //    var lang = RouteData.Values["language"].ToString();
        //    var actionName = descriptor.ActionName;
        //    var controllerName = descriptor.ControllerName;

        //    var name =(actionName.ToLower() == "index" ? controllerName : actionName);
            

        //    var cultureFromUrl = (string)RouteData.Values["language"];
        //    var cultureFromCookie = Request.Cookies[CookieRequestCultureProvider.DefaultCookieName] ?? string.Empty;

        //    var cultureName = CultureHandler.GetImplementedCulture(cultureFromUrl);

        //    // Deserialize cookie value to get culture name
        //    if (!string.IsNullOrEmpty(cultureFromCookie) && cultureFromCookie.Contains("|"))
        //    {
        //        cultureFromCookie = cultureFromCookie.Split('|')[0].Split('=')[1];
        //    }

        //    if (cultureFromUrl == CultureHandler.GetUnknownCulture())
        //    {
        //        cultureName = CultureHandler.GetImplementedCulture(!string.IsNullOrEmpty(cultureFromCookie) ?
        //            cultureFromCookie : CultureHandler.GetDefaultCulture());

        //        RouteData.Values["language"] = CultureHandler.GetNeutralCulture(cultureName);
        //    }
        //    else if (cultureName != cultureFromCookie)
        //    {
        //        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
        //            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
        //            new CookieOptions
        //            {
        //                Expires = DateTimeOffset.UtcNow.AddMonths(1)
        //            });
        //    }

        //    // Modify current thread's cultures            
        //    var cultureInfo = new CultureInfo(cultureName);
        //    CultureInfo.CurrentCulture = cultureInfo;
        //    CultureInfo.CurrentUICulture = cultureInfo;
        //    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        //    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        //    Page pageModel = null;
        //    pageData.PageInfo.TryGetValue( name.ToLower(), out pageModel);
        //    var page = Mapper.Map<PageViewModel>(pageModel);
        //    if (page != null && !string.IsNullOrEmpty(page.PageName))
        //    {
        //        ViewData[MetaKeywords.MetaTitle] = page.Title_Translation[lang];
        //        ViewData[MetaKeywords.MetaDescription] = page.Description_Translation[lang];
        //        ViewData[MetaKeywords.MetaKeys] = page.Keys_Translation[lang];
        //    }
        //    base.OnActionExecuting(context);
        //}

        //protected ViewResult BadRequestView(object model)
        //{
        //    Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //    return View(model);
        //}
    }
}