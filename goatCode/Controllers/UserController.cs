using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using goatCode.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            pservice.UserIndexHelper(User.Identity.Name);
            var ret = pservice.GetProjectsOwnedByUser(User.Identity.Name);
            ViewBag.NotOwned = pservice.UserIndexHelper(User.Identity.Name);
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
        [ValidateInput(false)]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.name = HttpUtility.HtmlEncode(project.name);

                pservice.AddNewProject(project, User.Identity.GetUserId());

                return RedirectToAction("Index");
            }
            return View(project);
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
            if(uservice.IsUserRelatedToProject(base.User.Identity.GetUserId(), ProjectId.Value))
            {
               return View(new ShareViewModel { projectId = ProjectId.Value }); 
            }            
            // ef að notandi er ekki tengdur við project eða projectid == null
            return View("ProjectPermissionError");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ShareProjects(ShareViewModel model)
        {
            model.email = HttpUtility.HtmlEncode(model.email);

            if (uservice.DoesUserExist(model.email))
            {
                if (uservice.IsUserOwner(uservice.GetUserIdByName(model.email), model.projectId) == true)
                {
                    // Ef user er owner þá getur hann ekki share-að
                    return View("UserIsOwnerError");
                }
                else if (uservice.IsUserRelatedToProject(uservice.GetUserIdByName(model.email), model.projectId) == true)
                {
                    // Ef user er nú þegar tengdur við project þá gerist ekkert
                    return View("CantShareWithThisUserError");
                }
                else
                {
                    pservice.AddUserToProject(model);
                    return RedirectToAction("Index");
                }
            }
            // Notandi sem reynt var að deila með er ekki til
            return View("UserDoesNotExistError");
        }

        /// <summary>
        /// User can rename the projects name.
        /// </summary>
        /// <param name="projectId">Is used to get value of the parameter projectId.</param>
        /// <returns>First view to rename the project - then it returns view of all projects.</returns>
        [HttpGet]
        public ActionResult Edit(int? projectId)
        {
            if (uservice.IsUserOwner(base.User.Identity.GetUserId(), projectId.Value))
            {
                return View(pservice.GetProjectByProjectId(projectId.Value));
            }
            // EF að Notandi er ekki eigandi að project eða project == null
            return View("ProjectPermissionError");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Project project)
        {
            project.name = HttpUtility.HtmlEncode(project.name);
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
            
            if (uservice.IsUserRelatedToProject(base.User.Identity.GetUserId(), projectId.Value))
            {
                if (uservice.IsUserOwner(base.User.Identity.GetUserId(), projectId.Value))
                {
                    // Delete All Files
                    fservice.DeleteAllFilesinProject(projectId.Value);
                    // Delete All Relations
                    uservice.DeleteUserProjectRelations(projectId.Value);
                    uservice.DeleteUserOwnerRelations(base.User.Identity.GetUserId(), projectId.Value);
                    // Delete Project
                    pservice.DeleteProject(projectId.Value);

                    return RedirectToAction("Index");
                }
                else
                {
                    uservice.DeleteSingleUserProjectRelations(base.User.Identity.GetUserId(), projectId.Value);
                }
                return RedirectToAction("Index");
            }
            else
            {
                // Er ekki tengdur við project eða projectID == NULL
                return View("ProjectPermissionError");
            }
            
        }
        public ActionResult DownloadProjectAsZip(int? projectId)
        {
            if (uservice.IsUserRelatedToProject(User.Identity.GetUserId(), projectId.Value) == false)
            {
                return View("ProjectPermissionError");
            }
            else
            {

                var dir = fservice.GetFilesByProjectId(projectId.Value);
            var e = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = e + @"\" + pservice.GetProjectByProjectId(projectId.Value).name;

            Directory.CreateDirectory(path);

            foreach (var file in dir)
            {
                using (System.IO.StreamWriter data = new System.IO.StreamWriter(path + @"\" + file.name + "." + file.extension, true))
                {
                    data.Write(file.content);
                }

            }
            var s = pservice.GetProjectByProjectId(projectId.Value).name + ".zip";
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory(path);
                zip.Comment = "This zip was created at " + DateTime.Now.ToString("G") + " Using GoatCode";
                zip.Save(path + @"/" + s);
            }
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] thefile = System.IO.File.ReadAllBytes(path + @"/" + s);

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            di.Delete(true);

            return File(thefile, "zip", s);
        }

        }

        public ActionResult ProjectUsersList(int? projectId)
        {
            if(uservice.IsUserOwner(User.Identity.GetUserId(), projectId.Value))
            {
                var temp = new ProjectUsersViewModel();
                temp.projectId = projectId.Value;
                temp.list = uservice.GetProjectUsersByProjectId(projectId.Value, User.Identity.GetUserId());
                return View(temp);
            }
            return View("Error");
        }

        public ActionResult DeleteUserFromProject(string userId, int? projectId)
        {
            uservice.DeleteSingleUserProjectRelations(base.User.Identity.GetUserId(), projectId.Value);
            return RedirectToAction("Index");
        }
    }
}