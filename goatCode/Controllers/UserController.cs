using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles = "User")]
        public ActionResult Index(string userName)
        {
            if(userName == null || userName == "") // Prófum að skrifa inn í url username sem er ekki til. Ef vesen búa user validation
            {
                //TODO: Return View með ViewModeli
            }
            return View();
        }
    }
}