﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return Content("<h2>Users</h2>", "text/html");
        }
    }
}