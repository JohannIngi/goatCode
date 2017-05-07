using goatCode.Services;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private FileService fservice = new FileService();
        private ProjectService pservice = new ProjectService();
        private UserService uservice = new UserService();
        /// <summary>
        /// Index 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Files = fservice.GetAllFiles().Count;
            ViewBag.Projects = pservice.GetAllProjects().Count();
            ViewBag.Users = uservice.GetAllUsers().Count;
            
            return View();
        }
        public ActionResult Files()
        {
            var model = fservice.GetAllFiles();
            return View(model);
        }
        public ActionResult Projects()
        {
            var model = pservice.GetAllProjects();
            return View(model);
        }
        public ActionResult Users()
        {
            var model = uservice.GetAllUsers();
            return View(model);
        }
    }
}