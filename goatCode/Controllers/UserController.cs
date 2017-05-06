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
        private UserProjectService upservice = new UserProjectService();
        private ProjectOwnerService poservice = new ProjectOwnerService();
        // GET: User
        [Authorize(Roles = "User")]
        public ActionResult Index(string userName) // TODO: þarf ekki þetta - höfum alltaf aðgang með Users.Identity.Namme
        {
            if(userName != null || userName != "") // Prófum að skrifa inn í url username sem er ekki til. Ef vesen búa user validation
            {
                var ret = pservice.getInUseProjectsByUserName(userName);
                return View(ret);
            }
            //TODO: specific error view
            return View("Error");
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
            pservice.AddNewProject(project);

            UserProject newProject = new UserProject();
            newProject.userId = User.Identity.GetUserId();
            newProject.projectId = pservice.GetProjectIdByName(project);
            upservice.addUserProjectConnection(newProject);

            ProjectOwner newOwner = new ProjectOwner();
            newOwner.projectId = pservice.GetProjectIdByName(project);
            newOwner.userId = User.Identity.GetUserId();

            poservice.addNewProjectOwner(newOwner);

            return RedirectToAction("Index", new { username = User.Identity.Name });
        }
        [HttpGet]
        public ActionResult ShareProjects(int? ProjectId)
        {
            if (ProjectId.HasValue)
            {
                return View(new ShareViewModel { projectId = ProjectId.Value });
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult ShareProjects(ShareViewModel model)
        {
            if(uservice.DoesUserExist(model.email))
            {
                pservice.AddUserToProject(model);
                return RedirectToAction("Index", new { userName = User.Identity.Name });
            }
            return View("Error");
        }
    }
}