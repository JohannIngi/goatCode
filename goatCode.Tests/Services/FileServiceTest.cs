using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using goatCode.Services;
using goatCode.Tests.Util;
using goatCode.Models.Entities;
using System.Collections.Generic;

namespace goatCode.Tests.Services
{
    [TestClass]
    public class FileServiceTest
    { 
    
        private FileService fileService;
       

        [TestInitialize]
        public void Initialize()
        {
            var mock = new MockDatabase();

            mock.Projects.Add(new Project { ID = 1, name = "project1" });
            mock.Projects.Add(new Project { ID = 2, name = "project2" });
            mock.Projects.Add(new Project { ID = 3, name = "project3" });
            mock.Projects.Add(new Project { ID = 4, name = "project4" });

            mock.Files.Add(new File { ID = 1, name = "file1", extension = "c", content = "abc1" });
            mock.Files.Add(new File { ID = 2, name = "file2", extension = "cpp", content = "abc2" });
            mock.Files.Add(new File { ID = 3, name = "file3", extension = "java", content = "abc3" });
            mock.Files.Add(new File { ID = 4, name = "file4", extension = "hs", content = "abc4" });

            mock.ProjectFiles.Add(new ProjectFile { id = 1, fileId = 1, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 2, fileId = 2, projectId = 2 });
            mock.ProjectFiles.Add(new ProjectFile { id = 3, fileId = 3, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 4, fileId = 4, projectId = 1 });

            fileService = new FileService(mock);
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var query1 = fileService.GetSingleFileById(1);
            Assert.AreEqual(1, query1.ID);

            fileService.DeleteFile(1);

            var query2 = fileService.GetSingleFileById(1);
            Assert.AreEqual(1, query2.ID);
        }

        [TestMethod] 
        public void DeleteAllFilesinProjectTest()
        {
            fileService.DeleteAllFilesinProject(1);
            var query = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query)
            {
                idSet.Add(file.ID);
            }
            Assert.IsFalse(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsFalse(idSet.Contains(3));
            Assert.IsFalse(idSet.Contains(4));
        }

        [TestMethod]
        public void GetFilesByProjectIdTest1()
        {
            var query = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query)
            {
                idSet.Add(file.ID);
            }

            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.IsFalse(idSet.Contains(123));
            Assert.AreEqual(3, idSet.Count);
        }


        [TestMethod]
        public void GetFilesByProjectIdTest2()
        {
            var query = fileService.GetFilesByProjectId(3);
            Assert.AreEqual(0, query.Count);
        }

        [TestMethod]
        public void RemoveFileProjectConnectionTest()
        {
           fileService.RemoveFileProjectConnection(1);

            var query = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query)
            {
                idSet.Add(file.ID);
            }

            Assert.IsFalse(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.AreEqual(2, idSet.Count);
        }

        [TestMethod]
        public void GetSingleFileByIdTest()
        {
            var query = fileService.GetSingleFileById(0);
            var query2 = fileService.GetSingleFileById(1);
            Assert.AreNotEqual(0, query);
            Assert.AreEqual(1, query2.ID);


        }

        [TestMethod]
        public void GetAllFilesTest()
        {
            var query = fileService.GetAllFiles();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in query)
            {
                idSet.Add(file.ID);
            }
            Assert.IsTrue(idSet.Contains(1));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
        }
    }
}
