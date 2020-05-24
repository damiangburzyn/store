using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using GC5.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace System
{
    public static class ControllerExtension
    {

        //public static ILoggerFactory LoggerFactory { get; set; } = new LoggerFactory();
        //internal static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
        //internal static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
        //private static ILogger logger = CreateLogger("ControllerExtension");

        public static async Task<T> WrapExceptionAsync<T>(this ControllerBase contr, Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                SingleLogger.Instance.Logger.LogError(ex, ex.Message, null);
                throw;
            }
        }

        public static IActionResult WrapException<IActionResult>(this ControllerBase contr, Func<IActionResult> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                SingleLogger.Instance.Logger.LogError(ex, ex.Message, null);
                throw;
            }
        }


        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.GetView(viewName, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

    }
}
