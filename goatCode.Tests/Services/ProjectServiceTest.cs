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
    public class ProjectServiceTest
    {
        private ProjectService projectService;
        private UserService userv;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new MockDatabase();

            mock.Projects.Add(new Project { ID = 1, name = "project1" });
            mock.Projects.Add(new Project { ID = 2, name = "project2" });
            mock.Projects.Add(new Project { ID = 3, name = "project3" });
            mock.Projects.Add(new Project { ID = 4, name = "project4" });

            mock.Users.Add(new ApplicationUser {Id = "1231", Email = "a1@a.com", UserName = "a1@a.com", PasswordHash = "a1", SecurityStamp = "b1" });
            mock.Users.Add(new ApplicationUser { Id = "1232", Email = "a2@a.com", UserName = "a2@a.com", PasswordHash = "a2", SecurityStamp = "b2" });
            mock.Users.Add(new ApplicationUser { Id = "1233", Email = "a3@a.com", UserName = "a3@a.com", PasswordHash = "a3", SecurityStamp = "b3" });
            mock.Users.Add(new ApplicationUser { Id = "1234", Email = "a4@a.com", UserName = "a4@a.com", PasswordHash = "a4", SecurityStamp = "b4" });


            mock.ProjectOwners.Add(new ProjectOwner { id = 1, userId = "1231", projectId = 1 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 2, userId = "1231", projectId = 2 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 3, userId = "1231", projectId = 3 });
            mock.ProjectOwners.Add(new ProjectOwner { id = 4, userId = "1234", projectId = 4 });

            mock.Files.Add(new File { ID = 1, name = "file1", extension = "c", content = "abc1" });
            mock.Files.Add(new File { ID = 2, name = "file2", extension = "cpp", content = "abc2" });
            mock.Files.Add(new File { ID = 3, name = "file3", extension = "java", content = "abc3" });
            mock.Files.Add(new File { ID = 4, name = "file4", extension = "hs", content = "abc4" });

            mock.ProjectFiles.Add(new ProjectFile { id = 1, fileId = 1, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 2, fileId = 2, projectId = 2 });
            mock.ProjectFiles.Add(new ProjectFile { id = 3, fileId = 3, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 4, fileId = 4, projectId = 1 });

            userv = new UserService(mock);
            projectService = new ProjectService(mock);
        }

        [TestMethod]
        public void UserTest()
        {
            var query = userv.DoesUserExist("a1@a.com");
            Assert.IsTrue(query);
        }

        [TestMethod]
        public void GetProjectByProjectIdTest1()
        {
            var query = projectService.GetProjectByProjectId(1);
            Assert.AreNotEqual(0, query.ID);
            Assert.AreEqual(1, query.ID);
            Assert.AreNotEqual(2, query.ID);
        }

        [TestMethod]
        public void GetProjectIdByFileIdTest1()
        {
            var query = projectService.GetProjectIdByFileId(4);
            Assert.AreEqual(1, query);
        }

        [TestMethod]
        public void GetProjectIdByFileIdTest2()
        {
            var query = projectService.GetProjectIdByFileId(0);
            Assert.AreNotEqual(1, query);
        }

        [TestMethod]
        public void GetProjectsOwnedByUserProjectTest()
        {
            var query = projectService.GetProjectsOwnedByUser("a1@a.com");

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query)
            {
                idSet.Add(file.ID);
            }

            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(4));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsFalse(idSet.Contains(0));
            Assert.AreEqual(3, query.Count);
            Assert.AreNotEqual(0, query.Count);
        }


        [TestMethod]
        public void DeleteProjectTest()
        {
            var query1 = projectService.GetProjectByProjectId(1);
            Assert.AreEqual(1, query1.ID);

            projectService.DeleteProject(1);

            var query2 = projectService.GetAllProjects();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query2)
            {
                idSet.Add(file.ID);
            }

            Assert.IsFalse(idSet.Contains(1));
            Assert.IsTrue(idSet.Contains(2));
        }

        [TestMethod]
        public void AddNewProject()
        {
            Project proj = new Project { ID = 5, name = "project5" };
            var query1 = projectService.GetProjectByProjectId(5);
            Assert.AreNotEqual(5, query1);

            projectService.AddNewProject(proj, "1231");

            var query2 = projectService.GetAllProjects();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query2)
            {
                idSet.Add(file.ID);
            }
            Assert.IsTrue(idSet.Contains(5));
        }

        [TestMethod]
        public void EditProjectNameTest()
        {
            Project proj = new Project { ID = 6, name = "project6" };
            projectService.AddNewProject(proj, "1231");
            var query = projectService.GetAllProjects();
            
            List<String> nameList = new List<String>();
            foreach(var file in query)
            {
                nameList.Add(file.name);
            }

            Assert.IsTrue(nameList.Contains("project6"));

            proj.name = "asdf";

            projectService.EditProjectName(proj);
            List<String> nameList2 = new List<String>();
            foreach (var file in query)
            {
                nameList2.Add(file.name);
            }
            Assert.IsTrue(nameList2.Contains("asdf"));
        }
    }
}
