using goatCode.Models.ViewModels;
using goatCode.Services;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using goatCode.Models.Entities;
using System.Text;

namespace goatCode.Controllers
{
    public class ProjectsController : Controller
    {
        private ExtensionService _utservice = new ExtensionService();
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
            if(projectId.HasValue && _uservice.IsUserRelatedToProject(User.Identity.GetUserId(), projectId.Value))
            {
                var model = new ProjectIndexViewModel()
                {
                    files = _fservice.GetFilesByProjectId(projectId.Value),
                    projectId = projectId.Value,
                    name = _pservice.GetProjectByProjectId(projectId.Value).name
                };

                ViewBag.Owner = _uservice.IsUserOwner(User.Identity.GetUserId(), projectId.Value);


                return View(model);
            }
            else
            {
                return View("ProjectPermissionError"); // Anton reddar þessu error viewi
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
            
            _fservice.UpdateFile(file.content, file.ID);
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
               
            }
           
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
            if(_uservice.IsUserRelatedToProject(User.Identity.GetUserId(), ProjectId.Value))
            {
                var model = new NewFileViewModel { ProjectId = ProjectId.Value };

                ViewBag.Extensions = new SelectList(_utservice.PopulateDropDownList());
                return View(model);
            }
            return View("Error");  
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
            ViewBag.Extensions = new SelectList(_utservice.PopulateDropDownList());
            if (ModelState.IsValid)
            {
                //StringBuilder projName = new StringBuilder();
                //projName.Append(HttpUtility.HtmlEncode(model.name));
                //model.name = projName.ToString();
                string fileName = HttpUtility.HtmlEncode(model.name);
                model.name = fileName;
                
                if(_fservice.DoesFileNameExistInProject(model.ProjectId, model.name))
                {
                    ModelState.AddModelError("name", "File name already exists");
                    return View(model);
                }

                //model.name = Encoder.HtmlEncode(model.name, true);
                _fservice.AddNewFile(new File {extension = model.extension, name = model.name }, model.ProjectId);
            }
            return RedirectToAction("Index", new { ProjectId = model.ProjectId });
        }

        /// <summary>
        /// Downloads a specific file.
        /// </summary>
        /// <param name="fileId">Is used to get value of the parameter fileId</param>
        /// <returns>The file</returns>
        public FileResult DownloadFile(int fileId)
        {
            
            var dir = _fservice.GetSingleFileById(fileId);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] contentAsBytes = encoding.GetBytes(dir.content);                       

            return File(contentAsBytes, dir.extension, dir.name + "." + dir.extension);
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
                _fservice.RemoveFileProjectConnection(fileId.Value);
                _fservice.DeleteFile(fileId.Value);
            }

            if (projectId.HasValue)
            {
                return RedirectToAction("Index", new { projectId = projectId.Value });
            }

            return View("Error");
        }
        /// <summary>
        /// Gets the new name to update and the file id and project id.
        /// </summary>
        /// <param name="fileId">Is used to get value of the parameter fileId</param>
        /// <param name="projectId">Is used to get value of the parameter projectId</param>
        /// <returns>A view with the model that was made.</returns>
        [HttpGet]
        public ActionResult UpdateFileName(int? fileId, int? projectId)
        {
                if (fileId.HasValue && projectId.HasValue && _uservice.IsUserRelatedToProject(User.Identity.GetUserId(), projectId.Value))
                {
                    var file = _fservice.GetSingleFileById(fileId.Value);
                    var model = new FileUpdateViewModel()
                    {
                        ID = file.ID,
                        name = HttpUtility.HtmlEncode(file.name),
                        projectId = projectId.Value
                    };
                    return View(model);
                }
             
            return RedirectToAction("Error"); 
        }
        /// <summary>
        /// Callse the EditFileName function in the fileservice class.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateFileName(FileUpdateViewModel model)
        {
            if (ModelState.IsValid && !_fservice.DoesFileNameExistInProject(model.projectId, model.name))
            {
                _fservice.EditFileName(model);
                return RedirectToAction("Index", new { projectId = model.projectId });
            }
            ModelState.AddModelError("name", "File name already exists");
            return View(model);
        }
    }
}