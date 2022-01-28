using Microsoft.AspNetCore.Builder;

namespace ObserverPatternWeb.Middlewares
{
    public static class HttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMiddleware>();
        }
    }
}
