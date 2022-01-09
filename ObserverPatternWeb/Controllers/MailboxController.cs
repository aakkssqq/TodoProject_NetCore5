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
    public class MailboxController : BaseController
    {

        [ViewData]
        public CusUser User => OPUser;

        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult EmailView()
        {
            return View();
        }

        public IActionResult ComposeEmail()
        {
            return View();
        }

        public IActionResult EmailTemplates()
        {
            return View();
        }

        public IActionResult BasicActionEmail()
        {
            return View();
        }

        public IActionResult AlertEmail()
        {
            return View();
        }

        public IActionResult BillingEmail()
        {
            return View();
        }
    }
}