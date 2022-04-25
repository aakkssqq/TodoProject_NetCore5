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
    public class TablesController : BaseController
    {

        [ViewData]
        public CurrentUser User => OPUser;

        public IActionResult StaticTables()
        {
            return View();
        }

        public IActionResult DataTables()
        {
            return View();
        }

        public IActionResult FooTables()
        {
            return View();
        }

        public IActionResult jqGrid()
        {
            return View();
        }
    }
}