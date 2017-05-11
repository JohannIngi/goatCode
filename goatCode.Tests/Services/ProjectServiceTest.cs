using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using goatCode.Models;
using goatCode.Services;
using goatCode.Tests.Util;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
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
            mock.UserProjects.Add(new UserProject { id = 5, projectId = 3, userId = "1232" });
            mock.UserProjects.Add(new UserProject { id = 6, projectId = 4, userId = "1232" });

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
        public void GetProjectByProjectIdTest1()
        {
            var getProjectId = projectService.GetProjectByProjectId(1);
            Assert.AreNotEqual(0, getProjectId.ID);
            Assert.AreEqual(1, getProjectId.ID);
            Assert.AreNotEqual(2, getProjectId.ID);
        }

        [TestMethod]
        public void GetProjectIdByFileIdTest1()
        {
            var getProject = projectService.GetProjectIdByFileId(4);
            Assert.AreNotEqual(0, getProject);
            Assert.AreEqual(1, getProject);
            Assert.AreNotEqual(2, getProject);
        }

        [TestMethod]
        public void AddFileToProjectTest()
        {
            var q = projectService.GetProjectByProjectId(1);
            
            HashSet<int> idSet = new HashSet<int>();
            foreach(var file in idSet)
            {
                idSet.Add(1);
            }
            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.IsFalse(idSet.Contains(5));

            var newFile = new File { ID = 5, name = "file5", extension = "c", content = "abc5" };
        }

        [TestMethod]
        public void GetProjectsOwnedByUserProjectTest()
        {
            var projectsOwned = projectService.GetProjectsOwnedByUser("a1@a.com");

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in projectsOwned)
            {
                idSet.Add(file.ID);
            }

            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(4));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsFalse(idSet.Contains(0));
            Assert.AreEqual(3, projectsOwned.Count);
            Assert.AreNotEqual(0, projectsOwned.Count);
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            var getOne = projectService.GetProjectByProjectId(1);
            Assert.AreEqual(1, getOne.ID);

            projectService.DeleteProject(1);

            var getAll = projectService.GetAllProjects();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getAll)
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
            var getById = projectService.GetProjectByProjectId(5);
            Assert.AreNotEqual(5, getById);

            projectService.AddNewProject(proj, "1231");
            var getAll = projectService.GetAllProjects();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getAll)
            {
                idSet.Add(file.ID);
            }
            Assert.IsTrue(idSet.Contains(5));
        }

        [TestMethod]
        public void EditProjectNameTest()
        {
            var editProject = new Project { ID = 1, name = "editProject1" };
            var getById = projectService.GetProjectByProjectId(1);
            Assert.AreEqual("project1", getById.name);

            projectService.EditProjectName(editProject);

            var getEdit = projectService.GetProjectByProjectId(1);
            Assert.AreEqual("editProject1", getEdit.name);
        }

        [TestMethod]
        public void GetProjectsNotOwnedByUserTest()
        {
            var userProjects = projectService.GetProjectsNotOwnedByUser("a2@a.com");

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in userProjects)
            {
                idSet.Add(file.ID);
            }

            Assert.IsTrue(idSet.Contains(1));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
        }

        [TestMethod]
        public void AddUserToProjectTest()
        {
            var addUser = new ShareViewModel { email = "a3@a.com", projectId = 1 };
            projectService.AddUserToProject(addUser);
            var notOwned = projectService.GetProjectsNotOwnedByUser("a3@a.com");

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in notOwned)
            {
                idSet.Add(file.ID);
            }
            Assert.IsFalse(idSet.Contains(0));
            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
        }
    }
}
