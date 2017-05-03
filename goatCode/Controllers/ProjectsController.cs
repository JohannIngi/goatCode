using goatCode.Models.ViewModels;
using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectService _service = new ProjectService();
        // GET: Projects
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detailzz(int id)
        {
            var viewModel = _service.GetProjectsByID(id);
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProjectViewModel model = new ProjectViewModel();
            model.name = "";

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {


            return RedirectToAction("Index");
        }
    }
}