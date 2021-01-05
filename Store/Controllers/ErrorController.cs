using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Store.Controllers
{

    [ApiController]
    public class ErrorController : ControllerBase
    {
        private ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        public ILogger<ErrorController> Logger { get; private set; }

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
      [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            logger.LogError(context.Error, "Podczas działania aplikacji wystąpił błąd", null);
            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            logger.LogError(context.Error, "Podczas działania aplikacji wystąpił błąd", null);
            return Problem();
        }
    }
}
