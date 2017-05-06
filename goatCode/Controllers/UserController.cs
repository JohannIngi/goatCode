using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using goatCode.Services;
using Microsoft.AspNet.Identity;
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
        private UserService uservice = new UserService();

        
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var ret = pservice.getInUseProjectsByUserName(User.Identity.Name);
            return View(ret);
        }
       

        [HttpGet]
        public ActionResult Create()
        {
            Project project = new Project();
            return View(project);
        }
        [HttpPost]
        public ActionResult Create(Project project)
        {
            pservice.AddNewProject(project, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ShareProjects(int? ProjectId)
        {
            if (ProjectId.HasValue)
            {
                return View(new ShareViewModel { projectId = ProjectId.Value });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ShareProjects(ShareViewModel model)
        {
            if(uservice.DoesUserExist(model.email))
            {
                pservice.AddUserToProject(model);
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}