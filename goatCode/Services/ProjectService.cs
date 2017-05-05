using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public void AddNewProject(Project newProject)
        {
            _db.Projects.Add(newProject);
            _db.SaveChanges();
        }
        public int GetProjectIdByName(Project proj)
        {
            var projectz = _db.Projects;
            var projectId = (from p in projectz
                                 where p.name == proj.name
                                 select p.ID).SingleOrDefault();

            return projectId;
        }
        public Project GetSingleProjectById(int id)
        {
            var projectz = _db.Projects;

            Project singleProject = (from p in projectz
                                              where p.ID == id
                                              select p).SingleOrDefault();

            return singleProject;
        }
        public void DeleteProject(Project project)
        {
            _db.Entry(project).State = EntityState.Deleted;
            _db.SaveChanges();
            
        }
    }
}