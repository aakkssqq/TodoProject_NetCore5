using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObserverPattern.Base.Organization;

namespace ObserverPatternWeb.Controllers
{
    public class OrgController : BaseController
    {
        [ViewData]
        public CusUser User => OPUser;
        public IActionResult OrgPage()
        {
            return View();
        }
    }
}
