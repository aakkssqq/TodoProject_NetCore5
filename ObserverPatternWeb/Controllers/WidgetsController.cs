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
    public class WidgetsController : BaseController
    {
        [ViewData]
        public CusUser User => OPUser;

        public IActionResult Index()
        {
            return View();
        }
    }
}