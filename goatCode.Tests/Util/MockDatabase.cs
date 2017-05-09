using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using goatCode.Models.Entities;
using goatCode.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace goatCode.Tests.Util
{
    /// <summary>
    /// This is an example of how we'd create a fake database by implementing the 
    /// same interface that the BookeStoreEntities class implements.
    /// </summary>
    public class MockDatabase : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            this.Projects = new InMemoryDbSet<Project>();
            this.Files = new InMemoryDbSet<File>();
            this.ProjectFiles = new InMemoryDbSet<ProjectFile>();
            this.UserProjects = new InMemoryDbSet<UserProject>();
            this.ProjectOwners = new InMemoryDbSet<ProjectOwner>();
            this.Users = new InMemoryDbSet<ApplicationUser>();
            this.Roles = new InMemoryDbSet<IdentityRole>();
        }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<File> Files { get; set; }
        public IDbSet<ProjectFile> ProjectFiles { get; set; }
        public IDbSet<UserProject> UserProjects { get; set; }
        public IDbSet<ProjectOwner> ProjectOwners { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<IdentityRole> Roles { get; set; }
        

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
            //changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
            //changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

            return changes;
        }


        public void setModified(object entry)
        {
            // TODO: Manual updating
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}