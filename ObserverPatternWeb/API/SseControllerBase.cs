using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ObserverPatternWeb.API
{
    public class SseControllerBase: Controller
    {
        public object LocalizationSourceName { get; set; }
        protected SseControllerBase()
        {
            LocalizationSourceName = "Default";
        }
    }
}
