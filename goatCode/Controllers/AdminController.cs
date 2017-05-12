using goatCode.Models.ViewModels;
using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace goatCode.Controllers
{
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
            AdminSuperViewModel model = new AdminSuperViewModel()
            {
                allFiles = fservice.GetAllFiles().OrderBy(x => x.name).ToList(),
                allProjects = pservice.GetAllProjects().OrderBy(x => x.name).ToList(),
                allUsers = uservice.GetUserz().OrderBy(x => x.UserName).ToList(),
                statData = fservice.GetStatistics()

            };
            return View(model);
        }
        /// <summary>
        /// Admin can delete a project.
        /// It deletes all files in project, all relations to the project and then the project.
        /// </summary>
        /// <param name="projectId">Is used to get value of the parameter projectId</param>
        /// <returns>An Index page</returns>
        public ActionResult DeleteProject(int projectId)
        {
            fservice.DeleteAllFilesInProject(projectId);           
            uservice.DeleteUserProjectRelations(projectId);
            uservice.DeleteUserOwnerRelations(uservice.GetProjectOwnerIdByProjectId(projectId), projectId);        
            pservice.DeleteProject(projectId);

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Admin can delete a file.
        /// It deletes all connections to the file and then deletes the file.
        /// </summary>
        /// <param name="fileId">Is used to get value of the parameter fileId</param>
        /// <returns> An index page</returns>
        public ActionResult DeleteFile(int fileId)
        {
            fservice.RemoveFileProjectConnection(fileId);
            fservice.DeleteFile(fileId);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Admin can delete a user.
        /// It deletes all files in in his projects, then delets all project relations and then the projects.
        /// Then it deletes the User.
        /// </summary>
        /// <param name="userName">Is used to get value of the parameter userName</param>
        /// <returns>An Index page</returns>
        [HttpGet]
        public ActionResult DeleteUser(string userName)
        {
            foreach (var project in pservice.GetProjectsOwnedByUser(userName))
            {
                fservice.DeleteAllFilesInProject(project.ID);
                // Delete All Relations
                uservice.DeleteUserProjectRelations(project.ID);
                uservice.DeleteUserOwnerRelations(uservice.GetUserIdByName(userName), project.ID);
                // Delete Project
                pservice.DeleteProject(project.ID);
            }
            uservice.DeleteUser(userName);
            return RedirectToAction("Index");
        }
    }
}