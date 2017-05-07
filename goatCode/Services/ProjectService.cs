﻿using goatCode.Models;
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
        /// <summary>
        /// Instance of database.
        /// </summary>
        private ApplicationDbContext _db;

        public ProjectService()
        {
            _db = new ApplicationDbContext();
        }

        /// <summary>
        /// Gets a list of all projects connected with specific user name.
        /// </summary>
        /// <param name="userName">To let Users.UserName have the same value as parameter userName</param>
        /// <returns>A list of all projects a user is connected with.</returns>
        public List<Project> GetInUseProjectsByUserName(string userName)
        {
            return (from user in _db.Users
                    where user.UserName == userName
                    join pb in _db.UserProjects on user.Id equals pb.userId
                    join p in _db.Projects on pb.projectId equals p.ID
                    orderby p.name
                    select p).ToList();
        }

        /// <summary>
        /// Adds a new project to Projects table. The user that created the project will become the owner.
        /// The user(owner) and project will connect in ProjectOwner table.
        /// Also the user and project will connect in UserProject table. 
        /// </summary>
        /// <param name="newProject">To let ProjectOwners.projectId have the same value as parameter newProject.ID</param>
        /// <param name="uId">To let UserProjects.userId have the same value as parameter uId</param>
        public void AddNewProject(Project newProject, String uId)
        {
            _db.Projects.Add(newProject);
            _db.SaveChanges();

            _db.ProjectOwners.Add(new ProjectOwner { userId = uId, projectId = newProject.ID });
            _db.UserProjects.Add(new UserProject { userId = uId, projectId = newProject.ID });
            _db.SaveChanges();
        }

        /// <summary>
        /// User puts in email, if the email is in the database. User gets added to the UserProject table.
        /// And is therefore connected to the project and can view and edit it.
        /// </summary>
        /// <param name="model">Instance of ShareViewModel, to use parameters from ShareViewModel</param>
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

        /// <summary>
        /// If the projectId is in database, it will find the project it self and return it.
        /// </summary>
        /// <param name="projectId">To let Projects.ID have the same value as parameter projectId</param>
        /// <returns>The project it self.</returns>
        public Project GetProjectByProjectId(int projectId)
        {
            var project = _db.Projects.Where(x => x.ID == projectId).SingleOrDefault();
            return project;
        }

        /// <summary>
        /// User can edit name of selected project.
        /// </summary>
        /// <param name="project">Instance of Project class</param>
        public void EditProjectName(Project project)
        {
            _db.Entry(project).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// If selected project has ID in database, the projectID will be removed from the database and the Projects table. 
        /// It will also automatically be deleted from the connecting tables (UserProjects, ProjectFiles and ProjectOwners).
        /// </summary>
        /// <param name="projectId">To let Projects.ID have the same value as parameter projectId</param>
        public void DeleteProject(int projectId)
        {
            var project = _db.Projects.Where(x => x.ID == projectId).SingleOrDefault();
            _db.Projects.Remove(project);
            _db.SaveChanges();
        }
    }
}