using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections;
using System.Resources;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json.Linq;

namespace ObserverPatternWeb.API
{
    public class GlobalizationController : SseControllerBase
    {
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        //public IActionResult Index()
        //{

        //    JObject rv = new JObject();

        //    CultureInfo cultureInfo = CultureInfo.CurrentCulture; //YZLangHelper.CurrentCulture;
        //    string strlcid = "1035";//context.Request.Params["lcid"];
        //    int lcid;
        //    if (!String.IsNullOrEmpty(strlcid) && Int32.TryParse(strlcid, out lcid))
        //        cultureInfo = new CultureInfo(lcid);

        //    string assemblyName = "aaa";//request.GetString("assembly", null);
        //    string nameSpace = "555";//request.GetString("namespace", null) + "_";
        //    ResourceManager ret = new ResourceManager(typeof(Index)); // 当前类名index.
        //    string typeName = "Resources." + assemblyName;
        //    Type type = typeof(System.Resources.);
        //    ResourceManager mgr = new ResourceManager(typeName, type.Assembly);
        //    ResourceSet rsset = mgr.GetResourceSet(cultureInfo, true, true);

        //    JObject jsonStrings = new JObject();
        //    rv["strings"] = jsonStrings;

        //    IDictionaryEnumerator enumerator = rsset.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        string key = enumerator.Key as string;
        //        string value = enumerator.Value as string;

        //        if (key.StartsWith(nameSpace, true, null))
        //        {
        //            value = value.Replace("\\n", "\n");
        //            jsonStrings[key] = value;
        //        }
        //    }

        //    rv["test"] = "302678";
        //    rv["success"] = true;
        //    //return rv;
        //}
    }
}
