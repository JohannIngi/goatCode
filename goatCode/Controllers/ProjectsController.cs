using goatCode.Models.ViewModels;
using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using goatCode.Models.Entities;
using System.Diagnostics;

namespace goatCode.Controllers
{
    public class ProjectsController : Controller
    {
     //   private ProjectService _service = new ProjectService();
       // private UserProjectService _uservice = new UserProjectService();
        private FileService _fservice = new FileService();
        // GET: Projects
        public ActionResult Index(int? projectId)
        {
            if (projectId.HasValue)
            {
                var model = new ProjectIndexViewModel();
                model.files = _fservice.GetFilesByProjectId(projectId.Value);
                model.projectId = projectId.Value;
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult Save()
        {
            string content = Request.Form["Content"];
            _fservice.UpdateContent(content);
            return RedirectToAction("edit");
        }
        public ActionResult ReturnHome()
        {
            return View("index", "home");
        }
        public ActionResult Edit(int? FileId)
        {
            if (FileId.HasValue)
            {
                var file = _fservice.GetSingleFileById(FileId.Value);
                if (file != null)
                {
                    return View(file);
                }
                // TODO: hvað ef ekki til?
            }
            // TODO: akveda hvert a að fara annars
            return View("Error");
        }
        [HttpGet]
        public ActionResult Create(int? ProjectId)
        {
            var model = new NewFileViewModel { projectId = ProjectId.Value };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(NewFileViewModel model)
        {
           _fservice.AddNewFile(model);
 
            return RedirectToAction("Index", new { ProjectId = model.projectId });
        }



        // Gömul föll commenta þau út tímabundið
        /*public ActionResult UserProjects()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = _uservice.GetProjectByUser(userId);

            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProjectViewModel anton = new ProjectViewModel();
            anton.name = "";

            return View(anton);
        }
        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {
            Project anton = new Project();
            anton.name = model.name;
            _service.AddNewProject(anton);

            UserProject newProject = new UserProject();
            string userId = User.Identity.GetUserId();

            int projectId = _service.GetProjectIdByName(anton);

            newProject.projectId = projectId;
            newProject.userId = userId;
            _uservice.addUserProjectConnection(newProject);
            
            return RedirectToAction("UserProjects");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // TODO: Are you sure you want to Delete, cockbreath?

            Project model = _service.GetSingleProjectById(id);
            if(model != null)
            {
                _uservice.deleteProjectConnections(model);
                _service.DeleteProject(model);
                return RedirectToAction("UserProjects");
            }
            return HttpNotFound();
        }
 

        
        [HttpGet]
        public ActionResult AddNewFile(int? projectId)
        {
            
            FileViewModel newFile = new FileViewModel();
            newFile.name = "";
            newFile.projectId = projectId.Value;   

            return View(newFile);
        }
        [HttpPost]
        public ActionResult AddNewFile(FileViewModel model)
        {
            File anton = new File();
            anton.name = model.name;
            anton.projectID = model.projectId;
            _fservice.AddNewFile(anton);
            
            return RedirectToAction("Details", new { projectId = model.projectId });
        }
        public ActionResult RemoveFile(int id)
        {
            // TODO: Are you sure you want to Delete, cockbreath?
            File model = _fservice.GetSingleFileById(id);
            if(model != null)
            {
                int lastProject = model.projectID;
                _fservice.DeleteFile(model);
                return RedirectToAction("Details", lastProject);
            }
            return HttpNotFound();

        }
        */

        public ActionResult DeleteFile(int? fileId, int? projectId)
        {
            if (fileId.HasValue)
            {
                _fservice.DeleteFile(fileId.Value);
            }

            if (projectId.HasValue)
            {
                return RedirectToAction("Index", new { projectId = projectId.Value });
            }

            return View("Error");
        }

    }
}