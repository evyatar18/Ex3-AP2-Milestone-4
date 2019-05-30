using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // handles display/ip/port/frequency
            routes.MapRoute("display", "display/{ip}/{port}/{timePerSec}",
                defaults: new { controller = "Default", action = "Display" },
                constraints: new { ip = @"\d{0,3}.\d{0,3}.\d{0,3}.\d{0,3}" });

            // handles /display/filename/frequency
            routes.MapRoute("displaySaved", "display/{path}/{timePerSec}",
                new { controller = "Default", action = "DisplaySaved" });

            // handles /display/getdata
            routes.MapRoute("getData", "display/getdata",
                new { controller = "Default", action = "GetData" });

            // handles /save/ip/port/frequency/sec/path
            routes.MapRoute("save", "save/{ip}/{port}/{timePerSec}/{seconds}/{path}",
                new { controller = "Default", action = "Save" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
