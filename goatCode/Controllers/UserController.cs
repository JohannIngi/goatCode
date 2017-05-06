using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    public class UserController : Controller
    {
        private ProjectService pservice = new ProjectService();
        // GET: User
        [Authorize(Roles = "User")]
        public ActionResult Index(string userName)
        {
            if(userName == null || userName == "") // Prófum að skrifa inn í url username sem er ekki til. Ef vesen búa user validation
            {
                var ret = pservice.getInUseProjectsByUserName(userName);
                return View(ret);
            }
            //TODO: specific error view
            return View("Error");
        }
    }
}