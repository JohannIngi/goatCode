using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class UserService
    {
        /// <summary>
        /// Instance of database.
        /// </summary>
        private ApplicationDbContext _db;

        public UserService()
        {
            _db = new ApplicationDbContext();
        }

        /// <summary>
        /// Checking if user account is in the database. 
        /// </summary>
        /// <param name="email">Do let the Users.Email have the same value as parameter email</param>
        /// <returns>True if the email is the same as the email in database, otherwise false.</returns>
        public bool DoesUserExist(string email)
        {
            var user = _db.Users.Where(x => x.Email == email).SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check if specific userId is owner of a project. 
        /// </summary>
        /// <param name="userId">To let ProjectOwners.userId have the same value as parameter userId</param>
        /// <returns>If the userID is in ProjectOwners table it will return true, otherwise false.</returns>
        public bool IsUserOwner(string userId)
        {
            var owner = _db.ProjectOwners.Where(x => x.userId == userId).SingleOrDefault();
            if(owner == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Deleting a project relations from projectID. If selected projectID is in UserProjects table.
        /// The function will remove the ID from the table UserProjects.
        /// </summary>
        /// <param name="projectId">To let UserProjects.projectId have the same value as parameter projectId</param>
        public void DeleteUserProjectRelations(int projectId)
        {
            var userProject = _db.UserProjects.Where(x => x.projectId == projectId).ToList();
            foreach(var item in userProject)
            {
                _db.UserProjects.Remove(item);
            }
            _db.SaveChanges();
        }

        /// <summary>
        /// Both the userID and projectID have to be in the table ProjectOwners. 
        /// Together they have a projectOwnerID.
        /// Then the function will remove the projectOwnerID from table.
        /// </summary>
        /// <param name="userId">To let ProjectOwners.userId have the same value as parameter userId</param>
        /// <param name="projectId">To let ProjectOwners.projectId have the same value as parameter projectId</param>
        public void DeleteUserOwnerRelations(string userId, int projectId)
        {
            var projectOwner = _db.ProjectOwners.Where(x => x.userId == userId && x.projectId == projectId).SingleOrDefault();
            _db.ProjectOwners.Remove(projectOwner);
            _db.SaveChanges();
        }

        /// <summary>
        /// Both the userID and the projectID have to be in the table UserProjects.
        /// Together they have a userProjectID.
        /// This ID will then be removed from the table.
        /// </summary>
        /// <param name="userId">To let UserProjects.userId have the same value as parameter userId</param>
        /// <param name="projectId">To let UserProjects.projectId have the same value as parameter projectId</param>
        public void DeleteSingleUserProjectRelations(string userId, int projectId)
        {
            var userProject = _db.UserProjects.Where(x => x.projectId == projectId && x.userId == userId).SingleOrDefault();
            _db.UserProjects.Remove(userProject);
            _db.SaveChanges();
        }
    }
}