using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace ObserverPatternWeb.Middleware
{
    public static class HttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMiddleware>();
        }
    }
}
