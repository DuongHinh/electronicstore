﻿using System;
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
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

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
                 name: "List Product By Brand",
                 url: "{alias}.b-{brandId}",
                 defaults: new { controller = "Product", action = "ListByBrand", brandId = UrlParameter.Optional },
                 namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                 name: "New Category",
                 url: "{alias}.nc-{id}",
                 defaults: new { controller = "News", action = "Category", id = UrlParameter.Optional },
                 namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
               name: "News Detail",
               url: "{alias}.n-{id}",
               defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
               namespaces: new string[] { "ElectronicStore.Web.Controllers" }
           );

            routes.MapRoute(
               name: "SignUp",
               url: "sign-up",
               defaults: new { controller = "Account", action = "SignUp", id = UrlParameter.Optional },
               namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
               name: "SignIn",
               url: "sign-in",
               defaults: new { controller = "Account", action = "SignIn", id = UrlParameter.Optional },
               namespaces: new string[] { "ElectronicStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "shopping-cart",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddToCart",
                url: "add-to-cart",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Order",
               url: "order",
               defaults: new { controller = "Cart", action = "Order", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NotLoggedIn",
                url: "not-logged-in",
                defaults: new { controller = "Cart", action = "NotLoggedIn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OrderSuccess",
                url: "order-success",
                defaults: new { controller = "Cart", action = "OrderSuccess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OutOfStock",
                url: "out-of-stock",
                defaults: new { controller = "Cart", action = "OutOfStock", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About Us",
                url: "about-us",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
