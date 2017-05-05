using System.Collections.Generic;
using System.Linq;
using goatCode.Models;
using goatCode.Models.ViewModels;
using Microsoft.AspNet.Identity;
using goatCode.Models.Entities;
using System.Data.Entity;

namespace goatCode.Services
{
    public class UserProjectService
    {

        private ApplicationDbContext _db;

        public UserProjectService()
        {
            _db = new ApplicationDbContext();
        }

        /// <summary>
        /// Notað til að fá öll Projecct frá þeim notanda sem er skráður inn.
        /// </summary>
        /// <returns></returns>
        public List<ProjectViewModel> GetProjectByUser(string userId)
        {
            var userProjects = _db.UserProjects
            .Where(x => x.userId == userId)
            .Select(x => x.projectId)
            .ToList();

            List<ProjectViewModel> projectList = new List<ProjectViewModel>();
            var projectz = _db.Projects;
            foreach (var number in userProjects)
            {
                var singleProject = (from p in projectz
                            where p.ID == number
                            select p).SingleOrDefault();

                ProjectViewModel temp = new ProjectViewModel
                {
                    ID = singleProject.ID,
                    name = singleProject.name
                };
                projectList.Add(temp);
            }
            return projectList;
        }
        public void addUserProjectConnection(UserProject up)
        {
            _db.UserProjects.Add(up);
            _db.SaveChanges();
        }
        public void deleteProjectConnections(Project project)
        {
            foreach(var userproject in _db.UserProjects)
            {
                if(userproject.projectId == project.ID)
                {
                    _db.Entry(userproject).State = EntityState.Deleted;
                }
            }
            _db.SaveChanges();
        }
    }




}