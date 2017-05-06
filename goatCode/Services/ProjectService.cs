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
        public List<Project> getInUseProjectsByUserName(string userName)
        {
            return (from user in _db.Users
                    where user.UserName == userName
                    join pb in _db.UserProjects on user.Id equals pb.userId
                    join p in _db.Projects on pb.projectId equals p.ID
                    orderby p.name
                    select p).ToList();
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

        internal void AddUserToProject(ShareViewModel model)
        {
            // TODO: Hvað á að gerast ef notandi er þegar í verkefninu?
            var user = _db.Users.Where(x => x.Email == model.email).SingleOrDefault();
            if (user != null)
            {
                var entry = new UserProject { userId = user.Id, projectId = model.projectId };
                _db.UserProjects.Add(entry);
                _db.SaveChanges();
            }
        }
    }
}