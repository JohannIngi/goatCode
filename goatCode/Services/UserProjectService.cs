using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goatCode.Models;
using goatCode.Models.ViewModels;
using System.Web.Security;
using goatCode.Models.Entities;
using Microsoft.AspNet.Identity;


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
        public List<ProjectViewModel> GetProjectByUser()
        {
            string userId = "aids";
            
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
      

    }




}