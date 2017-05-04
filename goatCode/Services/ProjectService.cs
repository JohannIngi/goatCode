using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class ProjectService
    {
        private ApplicationDbContext _db;

        public ProjectService()
        {
            _db = new ApplicationDbContext();
        }

        public ProjectViewModel GetProjectsByID(int projectID)
        {
            var project = _db.Projects.SingleOrDefault(x => x.ID == projectID);

            if(project == null)
            {
                // TODO : Kasta Villu!
            }

            var filez = _db.Files
                .Where(x => x.projectID == projectID)
                .Select(x => new ProjectViewModel
                {
                    name = x.name,
                    ID = x.ID
                })
                .ToList();

            var viewModel = new ProjectViewModel
            {
                name = project.name,
                ID = project.ID
            };

            return viewModel;
        }
        

        public bool AddNewProject(Project newProject)
        {
            _db.Projects.Add(newProject);
            _db.SaveChanges();

            //TODO : villutjekk

            return true;
        }
    }
}