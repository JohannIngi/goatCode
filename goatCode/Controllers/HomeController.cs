using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                // ToDO: If is admin redirect to admin controller

                return RedirectToAction("Index", "User");
            }
            return View();
        }
    }
}