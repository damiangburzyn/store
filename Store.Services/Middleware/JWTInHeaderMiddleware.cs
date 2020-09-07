using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Store.Services.Middleware
{
    public class JWTInHeaderMiddleware
    {
        public const string AuthenticationCookieName = "access_token";
        private readonly RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
          
            var cookie = context.Request.Cookies[AuthenticationCookieName];
            if (cookie != null)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(cookie);
                context.Request.Headers.Append("Authorization", "Bearer " + token.TokenString);
            }

            await _next.Invoke(context);
        }
    }
}
