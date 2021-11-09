using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ObserverPatternWeb.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPatternWeb.Controllers
{
    public class HomeController : Controller
    {
        // 基於dotnet core的依賴注入，注入IMediator物件
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogWarning("123456");
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
