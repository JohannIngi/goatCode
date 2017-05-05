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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Máni er ekki sætur gaur";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to contact us mjéeen.";

            return View();
        }
        public ActionResult Editor()
        {
            ViewBag.Message = "I'm an editor, hello.";

            return View();
        }
        public ActionResult viewProjects()
        {
            return RedirectToAction("UserProjects");
        }
    }
}