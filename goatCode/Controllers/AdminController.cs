using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace goatCode.Controllers
{
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
        public ActionResult DeleteProject(int projectId)
        {
            pservice.DeleteProject(projectId);
            return RedirectToAction("Projects");
        }
        public ActionResult DeleteFile(int fileId)
        {
            fservice.DeleteFile(fileId);
            return RedirectToAction("Files");
        }

        [HttpGet]
        public ActionResult DeleteUser(string userName)
        {
            foreach (var project in pservice.GetProjectsOwnedByUser(userName))
            {
                fservice.DeleteAllFilesinProject(project.ID);
                // Delete All Relations
                uservice.DeleteUserProjectRelations(project.ID);
                uservice.DeleteUserOwnerRelations(uservice.GetUserIdByName(userName), project.ID);
                // Delete Project
                pservice.DeleteProject(project.ID);
            }
            uservice.DeleteUser(userName);
            return RedirectToAction("Users");
        }
    }
}