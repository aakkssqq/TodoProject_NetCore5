using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ObserverPattern.Base.Organization;

namespace ObserverPatternWeb.Controllers
{
    [Authorize]
    public class OrgController : BaseController
    {
        [ViewData]
        public CurrentUser User => OPUser;
        public IActionResult OrgPage()
        {
            return View();
        }
    }
}
