using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPatternWeb.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
    }
}
