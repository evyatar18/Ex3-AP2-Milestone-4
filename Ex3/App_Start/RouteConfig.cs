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

            routes.MapRoute("ad", "hey",
                defaults: new { controller = "Default", action="Hello" });
            routes.MapRoute("display", "display/{ip}/{port}/{timePerSec}",
                defaults: new { controller = "Default", action = "display" });
            //routes.MapRoute("display", "display/GetData",
            //    defaults: new { controller = "Default", action = "GetData" });
            //routes.MapRoute("display", "display/{path}/{timePerSec}");
            routes.MapRoute("save", "save/{ip}/{port}/{timePerSec}/{seconds}/{path}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
