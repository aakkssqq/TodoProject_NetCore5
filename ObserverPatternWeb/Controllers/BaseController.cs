using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ObserverPattern.Base.Organization;
using ObserverPatternWeb.Resources.OPStrings;

namespace ObserverPatternWeb.Controllers
{
    public class BaseController : Controller
    {
        protected virtual CusUser OPUser => new CusUser().GetUser(HttpContext?.User.Identity.Name);
        protected virtual string TestStr => "1234560";

        public BaseController()
        {
            ViewData["User"] = OPUser;
        }
    }
}
