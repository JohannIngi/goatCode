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
            mock.UserProjects.Add(new UserProject { id = 5, projectId = 1, userId = "1233" });
            mock.UserProjects.Add(new UserProject { id = 6, projectId = 1, userId = "1234" });

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
        public void GetAllUsersTest()
        {
            var getAll = userService.GetAllUsers();

            HashSet<string> emailSet = new HashSet<string>();
            foreach (var file in getAll)
            {
                emailSet.Add(file.email);
            }
            Assert.IsTrue(emailSet.Contains("a1@a.com"));
            Assert.IsTrue(emailSet.Contains("a2@a.com"));
            Assert.IsTrue(emailSet.Contains("a3@a.com"));
            Assert.IsTrue(emailSet.Contains("a4@a.com"));
            Assert.AreEqual(4, emailSet.Count);
        }

        [TestMethod]
        public void DoesUserExistTest1()
        {
            var user = userService.DoesUserExist("a1@a.com");
            Assert.IsTrue(user);
        }
        [TestMethod]
        public void DoesUserExistTest2()
        {
            var user = userService.DoesUserExist("bb@b.com");
            Assert.IsFalse(user);
        }

        [TestMethod]
        public void GetProjectUsersByProjectIdTest()
        {
            //Gets all users in project except owner
            var projectUsers = userService.GetProjectUsersByProjectId(1, "1231");

            HashSet<string> idSet = new HashSet<string>();
            foreach (var user in projectUsers)
            {
                idSet.Add(user.Id);
            }
            Assert.IsFalse(idSet.Contains("1231"));
            Assert.IsTrue(idSet.Contains("1232"));
            Assert.IsTrue(idSet.Contains("1233"));
            Assert.IsTrue(idSet.Contains("1234"));
            Assert.AreEqual(3, idSet.Count);
        }

        [TestMethod]
        public void IsUserOwnerTest1()
        {
            var owner1 = userService.IsUserOwner("1231", 1);
            var owner2 = userService.IsUserOwner("1232", 1);
            var onwer3 = userService.IsUserOwner("1231", 4);
            Assert.IsTrue(owner1);
            Assert.IsFalse(owner2);
            Assert.IsFalse(onwer3);
        }

        [TestMethod] 
        public void IsUserRelatedToProjectTest()
        {
            var user1 = userService.IsUserRelatedToProject("1231", 1);
            var user2 = userService.IsUserRelatedToProject("1231", 3);
            var user3 = userService.IsUserRelatedToProject("1233", 2);
            Assert.IsTrue(user1);
            Assert.IsFalse(user2);
            Assert.IsFalse(user3);
        }

        [TestMethod]
        public void DeleteUserOwnerRelationsTest()
        {
            var checkOwner = userService.IsUserOwner("1231", 1);
            Assert.IsTrue(checkOwner);

            userService.DeleteUserOwnerRelations("1231", 1);

            var checkDelete = userService.IsUserOwner("1231", 1);
            Assert.IsFalse(checkDelete);
        }
        [TestMethod] 
        public void DeleteUserProjectRelationsTest()
        {
            var userRelated1 = userService.IsUserRelatedToProject("1231", 1);
            var userRelated2 = userService.IsUserRelatedToProject("1232", 1);
            Assert.IsTrue(userRelated1);
            Assert.IsTrue(userRelated2);

            userService.DeleteUserProjectRelations(1);

            var userRelated3 = userService.IsUserRelatedToProject("1231", 1);
            var userRelated4 = userService.IsUserRelatedToProject("1232", 1);
            var userRelated5 = userService.IsUserRelatedToProject("1232", 2);
            var userRelated6 = userService.IsUserRelatedToProject("1231", 2);
            Assert.IsFalse(userRelated3);
            Assert.IsFalse(userRelated4);
            Assert.IsTrue(userRelated5);
            Assert.IsTrue(userRelated6);
        }

        [TestMethod]
        public void DeleteSingleUserProjectRelationsTest()
        {
            var userRelated = userService.IsUserRelatedToProject("1231", 1);
            var userRelated2 = userService.IsUserRelatedToProject("1232", 1);
            Assert.IsTrue(userRelated);
            Assert.IsTrue(userRelated2);

            userService.DeleteSingleUserProjectRelations("1231", 1);

            var userRelated3 = userService.IsUserRelatedToProject("1231", 1);
            var userRelated4 = userService.IsUserRelatedToProject("1232", 1);
            Assert.IsFalse(userRelated3);
            Assert.IsTrue(userRelated4);
        }

        [TestMethod] 
        public void GetUserIdByNameTest()
        {
            Assert.AreEqual("1231", userService.GetUserIdByName("a1@a.com"));
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var user = userService.DoesUserExist("a1@a.com");
            Assert.IsTrue(user);

            userService.DeleteUser("a1@a.com");

            var userDeleted = userService.DoesUserExist("a1@a.com");
            Assert.IsFalse(userDeleted);
        }

        [TestMethod]
        public void GetProjectOwnerIdByProjectIdTest()
        {
            var owner = userService.GetProjectOwnerIdByProjectId(1);
            Assert.AreEqual("1231", owner);
        }
    }
}
