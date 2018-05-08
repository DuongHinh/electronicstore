using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElectronicStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "contact-us",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "{alias}.p-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                 name: "Product Category",
                 url: "{alias}.pc-{id}",
                 defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                 namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                 name: "Search",
                 url: "search",
                 defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                 namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
