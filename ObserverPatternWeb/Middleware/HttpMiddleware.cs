using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ObserverPatternWeb.Middleware
{
    [Authorize]
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HttpMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyCustomMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("MyCustomMiddleware executing...");
            await _next(httpContext);
        }
    }
}
