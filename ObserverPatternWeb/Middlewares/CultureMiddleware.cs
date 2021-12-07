using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;

namespace ObserverPatternWeb.Middlewares
{
    public class CultureMiddleware
    {
        private static readonly List<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("ja"),
            new CultureInfo("zh"),
            new CultureInfo("zh-CHT"),
            new CultureInfo("zh-CHS")
        };

        private static readonly RequestLocalizationOptions LocalizationOptions = new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture(_supportedCultures.First()),
            SupportedCultures = _supportedCultures,
            SupportedUICultures = _supportedCultures,
            RequestCultureProviders = new[]
            {
                new RouteDataRequestCultureProvider() { Options = LocalizationOptions }
            }
        };

        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestLocalization(LocalizationOptions);
        }
    }
}
