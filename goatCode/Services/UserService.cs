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
        private ApplicationDbContext _db;

        public UserService()
        {
            _db = new ApplicationDbContext();
        }
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

        public List<AdminUsersViewModel> GetAllUsers()
        {
            var role = _db.Roles.SingleOrDefault(x => x.Name == "Admin");
            var model = _db.Users.Where(x => x.Roles.All(r => r.RoleId != role.Id));
            var retValue = model.Select(s => new AdminUsersViewModel { email = s.Email }).ToList();

            return retValue;
        }
   
        public bool IsUserOwner(string userId, int projectId)
        {
            var owner = _db.ProjectOwners.Where(x => x.userId == userId && x.projectId == projectId).SingleOrDefault();
            if(owner == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsUserRelatedToProject(string userId, int projectId)
        {
            var relation = _db.UserProjects.Where(x => x.userId == userId && x.projectId == projectId).SingleOrDefault();
            if (relation == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void DeleteUserProjectRelations(int projectId)
        {
            var userProject = _db.UserProjects.Where(x => x.projectId == projectId).ToList();
            foreach(var item in userProject)
            {
                _db.UserProjects.Remove(item);
            }
            _db.SaveChanges();
        }
        public void DeleteUserOwnerRelations(string userId, int projectId)
        {
            var projectOwner = _db.ProjectOwners.Where(x => x.userId == userId && x.projectId == projectId).SingleOrDefault();
            _db.ProjectOwners.Remove(projectOwner);
            _db.SaveChanges();
        }
        public void DeleteSingleUserProjectRelations(string userId, int projectId)
        {
            var userProject = _db.UserProjects.Where(x => x.projectId == projectId && x.userId == userId).SingleOrDefault();
            _db.UserProjects.Remove(userProject);
            _db.SaveChanges();
            
        }
        public string GetUserIdByName(string userName)
        {
            return _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).SingleOrDefault();
        }
        public void DeleteUser(string userName)
        {
            _db.Users.Remove(_db.Users.Where(x => x.UserName == userName).SingleOrDefault());
        }
    }
}