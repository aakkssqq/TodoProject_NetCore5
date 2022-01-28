using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObserverPattern.Base.Organization;

namespace ObserverPatternWeb.Controllers
{
    [Authorize]
    public class GraphsController : BaseController
    {
        [ViewData]
        public CusUser User => OPUser;

        public IActionResult Flot()
        {
            return View();
        }

        public IActionResult Morris()
        {
            return View();
        }

        public IActionResult Rickshaw()
        {
            return View();
        }

        public IActionResult Chartjs()
        {
            return View();
        }
        public IActionResult Chartist()
        {
            return View();
        }
        public IActionResult Peity()
        {
            return View();
        }

        public IActionResult Sparkline()
        {
            return View();
        }

        public IActionResult C3charts()
        {
            return View();
        }
    }
}