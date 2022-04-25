using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ObserverPattern.Base.Organization;
using ObserverPatternWeb.Resources.OPStrings;

namespace ObserverPatternWeb.Controllers
{
    [Authorize]
    public class DashboardsController : BaseController
    {
        private readonly UserManager<CusUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private IStringLocalizer<OPStringsResources> _localizer;

        [ViewData] 
        public CurrentUser User => OPUser;

        public DashboardsController(
            ILogger<HomeController> logger,
            IStringLocalizer<OPStringsResources> localizer,
            UserManager<CusUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<IActionResult> Dashboard_1()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard_2()
        {
            
            return View();
        }

        public IActionResult Dashboard_3()
        {
            return View();
        }

        public IActionResult Dashboard_4()
        {
            return View();
        }

        public IActionResult Dashboard_4_1()
        {
            return View();
        }

        public IActionResult Dashboard_5()
        {
            return View();
        }
    }
}