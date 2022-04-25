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
    public class GalleryController : BaseController
    {
        [ViewData]
        public CurrentUser User => OPUser;

        public IActionResult BasicGallery()
        {
            return View();
        }

        public IActionResult BootstrapCarusela()
        {
            return View();
        }

        public IActionResult SlickCarusela()
        {
            return View();
        }
    }
}