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
   
        /// <summary>
        /// Gets list of all projects connected to specific user name.
        /// </summary>
        /// <returns>Shows a list of all projects</returns>
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var ret = pservice.GetInUseProjectsByUserName(User.Identity.Name);
            return View(ret);
        }

        /// <summary>
        /// This function first shows view of a display where user can fill in information
        /// to create a new project.
        /// After that the function calls AddNewProject from ProjectService and creates a
        /// new project. Then redirects the user to Index view.
        /// </summary>
        /// <returns>Index view (list of all projects)</returns>
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

        /// <summary>
        /// User can share project with another user. He has to fill in email, and the
        /// email has to be in the database already.
        /// </summary>
        /// <param name="ProjectId">Lets projectID from ShareViewModel have same value as parameter ProjectId</param>
        /// <returns>First view to fill in email - then after email is valid. View of project list.</returns>
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

        /// <summary>
        /// User can rename the projects name.
        /// </summary>
        /// <param name="projectId">Is used to get value of the parameter projectId.</param>
        /// <returns>First view to rename the project - then it returns view of all projects.</returns>
        [HttpGet]
        public ActionResult Edit(int? projectId)
        { 
            return View(pservice.GetProjectByProjectId(projectId.Value));
        }
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            pservice.EditProjectName(project);

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// User can delete a project. 
        /// It deletes all files in project, all relations to the project and then the project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>View of all projects (except project the user deleted).</returns>
        public ActionResult Delete(int? projectId)
        {
            if(projectId.HasValue)
            {
                if (uservice.IsUserOwner(User.Identity.GetUserId()))
                {
                    // Delete All Files
                    fservice.DeleteAllFilesinProject(projectId.Value);
                    // Delete All Relations
                    uservice.DeleteUserProjectRelations(projectId.Value);
                    uservice.DeleteUserOwnerRelations(User.Identity.GetUserId(), projectId.Value);
                    // Delete Project
                    pservice.DeleteProject(projectId.Value);
                }
                else
                {
                    uservice.DeleteSingleUserProjectRelations(User.Identity.GetUserId(), projectId.Value);                   
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}