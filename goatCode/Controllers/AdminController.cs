using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminController : Controller
    {
        // GET: Admin

        /// <summary>
        /// Displays an index page.
        /// </summary>
        /// <returns>An index page</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}