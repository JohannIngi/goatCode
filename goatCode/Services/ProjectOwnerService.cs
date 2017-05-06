using goatCode.Models;
using goatCode.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class ProjectOwnerService
    {
        private ApplicationDbContext _db;
        
        public ProjectOwnerService()
        {
            _db = ApplicationDbContext.Create();
        }
        public void addNewProjectOwner(ProjectOwner owner)
        {
            _db.ProjectOwners.Add(owner);
            _db.SaveChanges();
        }
    }
}