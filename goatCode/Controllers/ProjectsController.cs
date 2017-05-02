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
    }
}