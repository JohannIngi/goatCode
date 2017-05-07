﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    /// <summary>
    /// HomeController class will handle URL's to the homepage of our site.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays a login page if user doesn't have an account, but the homepage if the user has an account.
        /// </summary>
        /// <returns>View of the homepage if user is logged in</returns>
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        public ActionResult Register()
        {
            return RedirectToAction("Register", "Account");
        }
    }
}