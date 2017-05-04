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
        public List<ProjectViewModel> GetListOfProjects(List<int> projectIdArray)
        {
            List<ProjectViewModel> viddiErLegend = new List<ProjectViewModel>();
            var projectz = _db.Projects;
            foreach (var number in projectIdArray)
            {
                var atli = (from p in projectz
                            where p.ID == number
                            select p).SingleOrDefault();
                
                ProjectViewModel anton = new ProjectViewModel
                {
                    ID = atli.ID,
                    name = atli.name
                };
                viddiErLegend.Add(anton);                
            }   
            return viddiErLegend;
        }
    }
}