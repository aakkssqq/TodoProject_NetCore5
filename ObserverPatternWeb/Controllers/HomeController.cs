using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ObserverPatternWeb.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using ObserverPatternWeb.Resources.OPStrings;

namespace ObserverPatternWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStringLocalizer<OPStringsResources> _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<OPStringsResources> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogError("Test");
           
            return View();
        }

        //public IActionResult Index()
        //{
        //    // e.g. 訪問首頁時，傳送通知
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
