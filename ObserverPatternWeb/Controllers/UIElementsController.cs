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
    public class UIElementsController : BaseController
    {
        [ViewData]
        public CurrentUser User => OPUser;

        public IActionResult Typography()
        {
            return View();
        }

        public IActionResult Icons()
        {
            return View();
        }

        public IActionResult DraggablePanels()
        {
            return View();
        }

        public IActionResult ResizablePanels()
        {
            return View();
        }

        public IActionResult Buttons()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult TablesPanels()
        {
            return View();
        }

        public IActionResult Tabs()
        {
            return View();
        }

        public IActionResult NotificationsTooltips()
        {
            return View();
        }

        public IActionResult BadgesLabelsProgress()
        {
            return View();
        }

        public IActionResult HelperClasses()
        {
            return View();
        }
    }
}