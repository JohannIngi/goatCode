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
        private FileService fservice = new FileService();
   
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var ret = pservice.GetInUseProjectsByUserName(User.Identity.Name);
            return View(ret);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Project project = new Project();
            return View(project);
        }
        public ActionResult user()
        {
            return RedirectToAction("");
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
            if(uservice.IsUserRelatedToProject(User.Identity.GetUserId(), ProjectId.Value))
            {
               return View(new ShareViewModel { projectId = ProjectId.Value }); 
            }            
            // ef að notandi er ekki tengdur við project eða projectid == null
            return View("ProjectPermissionError");
        }
        [HttpPost]
        public ActionResult ShareProjects(ShareViewModel model)
        {
            if (uservice.DoesUserExist(model.email))
            {
                pservice.AddUserToProject(model);
                return RedirectToAction("Index");
            }
            // Notandi sem reynt var að deila með er ekki til
            return View("UserDoesNotExistError");
        }
        [HttpGet]
        public ActionResult Edit(int? projectId)
        {
            if (uservice.IsUserOwner(User.Identity.GetUserId(), projectId.Value))
            {
                return View(pservice.GetProjectByProjectId(projectId.Value));
            }
            // EF að Notandi er ekki eigandi að project eða project == null
            return View("ProjectPermissionError");
        }
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            pservice.EditProjectName(project);

            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int? projectId)
        {
            
            if (uservice.IsUserRelatedToProject(User.Identity.GetUserId(), projectId.Value))
            {
                if (uservice.IsUserOwner(User.Identity.GetUserId(), projectId.Value))
                {
                    // Delete All Files
                    fservice.DeleteAllFilesinProject(projectId.Value);
                    // Delete All Relations
                    uservice.DeleteUserProjectRelations(projectId.Value);
                    uservice.DeleteUserOwnerRelations(User.Identity.GetUserId(), projectId.Value);
                    // Delete Project
                    pservice.DeleteProject(projectId.Value);

                    return RedirectToAction("Index");
                }
                else
                {
                    uservice.DeleteSingleUserProjectRelations(User.Identity.GetUserId(), projectId.Value);
                }
                return RedirectToAction("Index");
            }
            else
            {
                // Er ekki tengdur við project eða projectID == NULL
                return View("ProjectPermissionError");
            }
            
        }
    }
}