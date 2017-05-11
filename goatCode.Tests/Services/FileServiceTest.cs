using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using goatCode.Services;
using goatCode.Tests.Util;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
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
            mock.Files.Add(new File { ID = 6, name = "test1", extension = "hs", content = "abc4" });
            mock.Files.Add(new File { ID = 7, name = "test2", extension = "hs", content = "abc4" });
            mock.Files.Add(new File { ID = 8, name = "test3", extension = "hs", content = "abc4" });

            mock.ProjectFiles.Add(new ProjectFile { id = 1, fileId = 1, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 2, fileId = 2, projectId = 2 });
            mock.ProjectFiles.Add(new ProjectFile { id = 3, fileId = 3, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 4, fileId = 4, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 5, fileId = 6, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 6, fileId = 7, projectId = 1 });
            mock.ProjectFiles.Add(new ProjectFile { id = 7, fileId = 8, projectId = 1 });

            fileService = new FileService(mock);
        }

        [TestMethod]
        public void AddFileToProjectTest()
        {
            var newFile = new File { ID = 5, name = "file5", extension = "c", content = "abc5" };

            fileService.AddNewFile(newFile, 1);
            Assert.IsFalse(fileService.DoesFileNameExistInProject(2, "file5"));
            Assert.IsTrue(fileService.DoesFileNameExistInProject(1, "file5"));
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var getFile = fileService.GetSingleFileById(2);
            Assert.AreEqual(2, getFile.ID);

            fileService.DeleteFile(2);

            var checkDeleted = fileService.GetSingleFileById(2);
            Assert.AreEqual(null, checkDeleted);
        }

        [TestMethod]
        public void UpdateFileTest()
        {
            var upFile = new File { ID = 1, name = "notFile1", extension = "cpp", content = "abc1123" };
            var getFile = fileService.GetSingleFileById(1);

            Assert.AreEqual("file1", getFile.name);

            fileService.UpdateFile(upFile);

            var checkUpdate = fileService.GetSingleFileById(1);
            Assert.AreEqual("notFile1", checkUpdate.name);
        }

        [TestMethod]
        public void DoesFileNameExistInProjectTest()
        {
            var exists = fileService.DoesFileNameExistInProject(1, "file1");
            var existsNot = fileService.DoesFileNameExistInProject(1, "file2");
            Assert.IsTrue(exists);
            Assert.IsFalse(existsNot);

        }

        [TestMethod]
        public void EditFileNameTest()
        {
            var editFile = new FileUpdateViewModel { ID = 1, projectId = 1, name = "editFile1" };
            var getFile = fileService.GetSingleFileById(1);
            Assert.AreEqual("file1", getFile.name);

            fileService.EditFileName(editFile);
            
            var getEdited = fileService.GetSingleFileById(1);
            Assert.AreEqual("editFile1", getEdited.name);           
        }

        [TestMethod]
        public void AddNewFileTest()
        {
            var newFile = new File { ID = 5, name = "file5", extension = "c", content = "abc5" };
            fileService.AddNewFile(newFile, 1);
            var getAll = fileService.GetAllFiles();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getAll)
            {
                idSet.Add(file.ID);
            }
            Assert.IsTrue(idSet.Contains(1));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.IsTrue(idSet.Contains(5));
        }

        [TestMethod]
        public void GetExtensionOccurrencesTest()
        {
            var stats1 = fileService.GetExtensionOccurrences("hs");
            var stats2 = fileService.GetExtensionOccurrences("c");
            Assert.AreEqual(4, stats1);
            Assert.AreEqual(1, stats2);
            
        }

        [TestMethod] 
        public void DeleteAllFilesinProjectTest()
        {
            fileService.DeleteAllFilesinProject(1);
            var getFiles = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getFiles)
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
            var getFiles = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getFiles)
            {
                idSet.Add(file.ID);
            }

            Assert.IsTrue(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.IsFalse(idSet.Contains(123));
            Assert.AreEqual(6, idSet.Count);
        }

        [TestMethod]
        public void RemoveFileProjectConnectionTest()
        {
           fileService.RemoveFileProjectConnection(1);

            var getFiles = fileService.GetFilesByProjectId(1);

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getFiles)
            {
                idSet.Add(file.ID);
            }

            Assert.IsFalse(idSet.Contains(1));
            Assert.IsFalse(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.AreEqual(5, idSet.Count);
        }

        [TestMethod]
        public void GetSingleFileByIdTest()
        {
            var getNot = fileService.GetSingleFileById(0);
            var getFile = fileService.GetSingleFileById(1);
            Assert.AreEqual(null, getNot);
            Assert.AreEqual(1, getFile.ID);
        }

        [TestMethod]
        public void GetAllFilesTest()
        {
            var getAll = fileService.GetAllFiles();

            HashSet<int> idSet = new HashSet<int>();
            foreach (var file in getAll)
            {
                idSet.Add(file.ID);
            }
            Assert.IsFalse(idSet.Contains(0));
            Assert.IsTrue(idSet.Contains(1));
            Assert.IsTrue(idSet.Contains(2));
            Assert.IsTrue(idSet.Contains(3));
            Assert.IsTrue(idSet.Contains(4));
            Assert.IsFalse(idSet.Contains(5));
            Assert.AreEqual(7, idSet.Count);
        }
    }
}
