using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gender_api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BulkRequest",
                url: "Request/Bulk/{key}/{names}",
                defaults: new { controller = "Request", action = "Bulk", names = UrlParameter.Optional, key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "singleRequest",
                url: "Request/Single/{key}/{name}",
                defaults: new { controller = "Request", action = "Single", name = UrlParameter.Optional, key = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            

            

        }
    }
}
