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

        public void AddNewProject(Project newProject, String uId)
        {
            _db.Projects.Add(newProject);
            _db.SaveChanges();

            _db.ProjectOwners.Add(new ProjectOwner { userId = uId, projectId = newProject.ID });
            _db.UserProjects.Add(new UserProject { userId = uId, projectId = newProject.ID });
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
        public void DeleteProject(int projectId)
        {
            var project = _db.Projects.Where(x => x.ID == projectId).SingleOrDefault();
            _db.Projects.Remove(project);
            _db.SaveChanges();
        }
    }
}