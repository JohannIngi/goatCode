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
        private ProjectService _service = new ProjectService();
        private UserProjectService _uservice = new UserProjectService();
        private FileService _fservice = new FileService();
        private int currentProject;
        // GET: Projects
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProjects()
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
        public ActionResult Details(int? projectId)
        {
            var viewModel = _fservice.GetFilesByProjectId(projectId.Value);
            if(viewModel != null)
            {
                currentProject = projectId.Value;
                return View(viewModel);
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult AddNewFile()
        {
            FileViewModel newFile = new FileViewModel();
            newFile.name = "";

            return View(newFile);
        }
        [HttpPost]
        public ActionResult AddNewFile(FileViewModel model)
        {
            File anton = new File();
            anton.name = model.name;
            anton.projectID = 69;
            anton.extensionId = 1;
            _fservice.AddNewFile(anton);
            
            return RedirectToAction("Details");
        }



    }
}