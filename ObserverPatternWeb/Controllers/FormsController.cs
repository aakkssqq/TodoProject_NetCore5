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
    public class FormsController : BaseController
    {
        [ViewData]
        public CusUser User => OPUser;

        public IActionResult BasicFroms()
        {
            return View();
        }

        public IActionResult Advanced()
        {
            return View();
        }

        public IActionResult Wizard()
        {
            return View();
        }

        public IActionResult FileUpload()
        {
            return View();
        }


        public IActionResult TextEditor()
        {
            return View();
        }

        public IActionResult Markdown()
        {
            return View();
        }

        public IActionResult Autocomplete()
        {
            return View();
        }
    }
}