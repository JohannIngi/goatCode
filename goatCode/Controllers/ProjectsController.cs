﻿using goatCode.Models.ViewModels;
using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using goatCode.Models.Entities;
using System.Diagnostics;
using System.Text;
using System.Web.Security;

namespace goatCode.Controllers
{
    public class ProjectsController : Controller
    {
       // private ProjectService _service = new ProjectService();
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService();
        private FileService _fservice = new FileService();
        // GET: Projects

        /// <summary>
        /// Displays the homepage, which has a list of all projects from specific user. If the user has no 
        /// project, no list will be displayed.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>A view with a list of all projects. 
        /// The view is from index in ProjectIndexViewModel</returns>
        public ActionResult Index(int? projectId)
        {
            if (projectId.HasValue)
            {
                var model = new ProjectIndexViewModel()
                {
                    files = _fservice.GetFilesByProjectId(projectId.Value),
                    projectId = projectId.Value
                };
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// This saves the content the user writes in the editor. There is a "Save" button in editor view.
        /// </summary>
        /// <returns>When the user saves, he will be moved automatically to another view</returns>
        [HttpGet]
        public ActionResult SaveCode(int? fileID)
        {
            var file = _fservice.GetSingleFileById(fileID.Value);
            return View(file);
        }
        [HttpPost]
        public ActionResult SaveCode(File file)
        {
            // TODO: Þetta virkar ekki þarf að skoða þetta betur seinna.
            
            _fservice.UpdateFile(file);
            return RedirectToAction("Edit", new { FileId = file.ID });
        }

        public ActionResult ReturnHome()
        {
            return View("index", "home");
        }

        /// <summary>
        /// When user clicks the edit button, he is moved to the editor view 
        /// where he can start programming and write his own content.
        /// </summary>
        /// <param name="FileId"></param>
        /// <returns>List of files if the file has ID that is not null. Otherwise it will return error</returns>

        public ActionResult Edit(int? FileId)
        {
            if (_uservice.IsUserRelatedToProject(User.Identity.GetUserId(), _pservice.GetProjectIdByFileId(FileId.Value)))
            {
                var file = _fservice.GetSingleFileById(FileId.Value);
                if (file != null)
                {
                    var model = new FileEditViewModel()
                    {
                        content = file.content,
                        extension = file.extension,
                        ID = file.ID,
                        name = file.name,
                        projectId = _pservice.GetProjectIdByFileId(FileId.Value)
                    };
                    ViewBag.DocumentID = FileId.Value;
                  
                    return View(model);
                }
                // TODO: hvað ef ekki til?
            }
            // TODO: akveda hvert a að fara annars
            return View("Error");
        }

        /// <summary>
        /// Gets projectId from NewFileViewModel. HttpPost will then create a new file in this specific project.
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>Views a list of all files in specific project.</returns>
        [HttpGet]
        public ActionResult Create(int? ProjectId)
        {
            var model = new NewFileViewModel { ProjectId = ProjectId.Value };
            
            ViewBag.Extensions = new SelectList(_fservice.PopulateDropDownList());
            return View(model);
        }

        /// <summary>
        /// Calls AddNewFile function in FileService and gives it projectId.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Views a list of all files in this specific project. </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = HttpUtility.HtmlEncode(model.name);
                model.name = fileName;

                if(_fservice.DoesFileNameExistInProject(model.ProjectId, model.name))
                {
                    ModelState.AddModelError("name", "File name already exists");
                }
                else
                {
                    _fservice.AddNewFile(model);
                    return RedirectToAction("Index", new { ProjectId = model.ProjectId });
                }
            }
            ViewBag.Extensions = new SelectList(_fservice.PopulateDropDownList());
            return View(model);
        }
       
        /// <summary>
        /// While the file (which user clicks) has ID, this calls the DeleteFile from FileService and it will
        /// delete the fileID from the database. 
        /// While the project user is in, has an ID, the user will be redirected to view with a list
        /// of all files (with the previously selected file deleted).
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="projectId"></param>
        /// <returns>View with all files and the selected file deleted. If the file has no ID, 
        /// this will return error.</returns>
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