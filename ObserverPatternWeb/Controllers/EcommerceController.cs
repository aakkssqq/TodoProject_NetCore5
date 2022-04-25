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
    public class EcommerceController : BaseController
    {
        [ViewData]
        public CurrentUser User => OPUser;

        public IActionResult ProductsGrid()
        {
            return View();
        }

        public IActionResult ProductsList()
        {
            return View();
        }

        public IActionResult ProductEdit()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        public IActionResult Payments()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}