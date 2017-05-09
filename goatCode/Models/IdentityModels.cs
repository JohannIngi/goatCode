using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using goatCode.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace goatCode.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public interface IAppDataContext
    {

        IDbSet<File> Files { get; set; }
        IDbSet<Project> Projects { get; set; }
        IDbSet<ProjectFile> ProjectFiles { get; set; }
        IDbSet<ProjectOwner> ProjectOwners { get; set; }
        IDbSet<UserProject> UserProjects { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        int SaveChanges();
        void setModified(object entry);
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public IDbSet<File> Files { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<ProjectFile> ProjectFiles { get; set; }
        public IDbSet<ProjectOwner> ProjectOwners { get; set; }
        public IDbSet<UserProject> UserProjects { get; set; }

        public void setModified(object entry)
        {
            Entry(entry).State = EntityState.Modified;
        }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<goatCode.Models.ViewModels.ProjectViewModel> ProjectViewModels { get; set; }

        public System.Data.Entity.DbSet<goatCode.Models.ViewModels.FileViewModel> FileViewModels { get; set; }

        public System.Data.Entity.DbSet<goatCode.Models.ViewModels.FileEditViewModel> FileEditViewModels { get; set; }

        public System.Data.Entity.DbSet<goatCode.Models.ViewModels.FileUpdateViewModel> FileUpdateViewModels { get; set; }
    }
}