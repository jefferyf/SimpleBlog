﻿using SimpleBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof (PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { controller = "auth", action = "login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "auth", action = "logout" }, namespaces);

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Posts", action = "Index" },
                namespaces: namespaces
            );
        }
    }
}
