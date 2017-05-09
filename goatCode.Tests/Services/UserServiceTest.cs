using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using goatCode.Models;
using goatCode.Services;
using goatCode.Tests.Util;
using goatCode.Models.Entities;
using System.Collections.Generic;

namespace goatCode.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService userService;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new MockDatabase();

            mock.Projects.Add(new Project { ID = 1, name = "project1" });
            mock.Projects.Add(new Project { ID = 2, name = "project2" });
            mock.Projects.Add(new Project { ID = 3, name = "project3" });
            mock.Projects.Add(new Project { ID = 4, name = "project4" });

            mock.Users.Add(new ApplicationUser { Id = "1231", Email = "a1@a.com", UserName = "a1@a.com", PasswordHash = "a1", SecurityStamp = "b1" });
            mock.Users.Add(new ApplicationUser { Id = "1232", Email = "a2@a.com", UserName = "a2@a.com", PasswordHash = "a2", SecurityStamp = "b2" });
            mock.Users.Add(new ApplicationUser { Id = "1233", Email = "a3@a.com", UserName = "a3@a.com", PasswordHash = "a3", SecurityStamp = "b3" });
            mock.Users.Add(new ApplicationUser { Id = "1234", Email = "a4@a.com", UserName = "a4@a.com", PasswordHash = "a4", SecurityStamp = "b4" });

            mock.ProjectOwners.Add(new ProjectOwner { id = 1, userId = "1231", projectId = 1 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 2, userId = "1231", projectId = 2 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 3, userId = "1231", projectId = 3 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 4, userId = "1234", projectId = 4 });

            mock.UserProjects.Add(new UserProject { id = 1, projectId = 1, userId = "1231" });
            mock.UserProjects.Add(new UserProject { id = 2, projectId = 1, userId = "1232" });
            mock.UserProjects.Add(new UserProject { id = 3, projectId = 2, userId = "1231" });
            mock.UserProjects.Add(new UserProject { id = 4, projectId = 2, userId = "1232" });

            mock.Files.Add(new File { ID = 1, name = "file1", extension = "c", content = "abc1" });
            mock.Files.Add(new File { ID = 2, name = "file2", extension = "cpp", content = "abc2" });
            mock.Files.Add(new File { ID = 3, name = "file3", extension = "java", content = "abc3" });
            mock.Files.Add(new File { ID = 4, name = "file4", extension = "hs", content = "abc4" });

            mock.ProjectFiles.Add(new ProjectFile { id = 1, fileId = 1, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 2, fileId = 2, projectId = 2 });
            mock.ProjectFiles.Add(new ProjectFile { id = 3, fileId = 3, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 4, fileId = 4, projectId = 1 });

            userService = new UserService(mock);
        }
        [TestMethod]
        public void DoesUserExistTest1()
        {
            var query = userService.DoesUserExist("a1@a.com");
            Assert.IsTrue(query);
        }
        [TestMethod]
        public void UDoesUserExistTest2()
        {
            var query = userService.DoesUserExist("bb@b.com");
            Assert.IsFalse(query);
        }
    }
}
