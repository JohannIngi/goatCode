﻿using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;


namespace goatCode.Services
{
    public class FileService
    {
        /// <summary>
        /// Instance of database.
        /// </summary>
        private readonly IAppDataContext _db;
        private ExtensionService utservice = new ExtensionService();

        public FileService()
        {
            _db = new ApplicationDbContext();
        }

        public FileService(IAppDataContext context)
        {
            _db = context;
        }

        /// <summary>
        /// If projectID is in Projects table. It searches for the same projectID in ProjectFiles table.
        /// When the projectID is found in ProjectFiles we take the fileID from the same ProjectFilesID.
        /// </summary>
        /// <param name="id">To match Projects.ID with the value of this id</param>
        /// <returns>Returns a list of all files in this projectID.</returns>
        public List<File> GetFilesByProjectId(int id)
        {
            return (from p in _db.Projects
                    where p.ID == id
                    join pf in _db.ProjectFiles on p.ID equals pf.projectId
                    join f in _db.Files on pf.fileId equals f.ID
                    orderby f.name
                    select f).ToList();
        }

        /// <summary>
        /// Add new file to selected project. The new file is added to the Files table aswell as 
        /// both the selected projectID and fileID is added to the ProjectFiles table.
        /// </summary>
        /// <param name="model">Instance of NewFileViewModel to get parameters from the ViewModel</param>
        public List<File> GetAllFiles()
        {
            return _db.Files.ToList();
        }

        public void AddNewFile(File model, int projectID)
        {
            model.content = utservice.GetStartContentForExtension(model.extension);
            _db.Files.Add(model);
            _db.SaveChanges();

            _db.ProjectFiles.Add(new ProjectFile { fileId = model.ID, projectId = projectID });
            _db.SaveChanges();
        }

        /// <summary>
        /// Gets a file from the Files table
        /// </summary>
        /// <param name="id">To let ID in Files get the same value as the parameter id</param>
        /// <returns>Returns single file from a known ID</returns>
        public File GetSingleFileById(int id)
        {
            return _db.Files.Where(x => x.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Searches by projectID to find a project, then removes all the files in specific project.
        /// </summary>
        /// <param name="projectId">So projectId in ProjectFiles table gets the same value as parameter projectId</param>

        public void DeleteAllFilesInProject(int projectId)
        {
            var files = _db.ProjectFiles.Where(x => x.projectId == projectId).ToList();
            foreach (var file in files)
            {
                _db.ProjectFiles.Remove(file);
            }
            _db.SaveChanges();
        }

        /// <summary>
        /// Updates content. The content in Files is something. After this the content will be the same as the user 
        /// writes in editor.
        /// </summary>
        /// <param name="content">Lets the content be same as the content in the parameter (from the editor)</param>

        public void UpdateFile(string content, int id)
        {
            var file = _db.Files.SingleOrDefault(x => x.ID == id);
            file.content = content;
            _db.setModified(file);
            _db.SaveChanges();
        }

        /// <summary>
        /// Finds a file with some ID and removes it from Files table.
        /// </summary>
        /// <param name="id">Lets the ID in file be the same as the parameter</param>
        public void DeleteFile(int id)
        {
            var file = GetSingleFileById(id);
            if (file != null)
            {
                _db.Files.Remove(file);
                _db.SaveChanges();
            }
        }
        /// <summary>
        /// User removes a file from project
        /// </summary>
        /// <param name="fileId"></param>
        public void RemoveFileProjectConnection(int fileId)
        {
            var projectfile = (from pf in _db.ProjectFiles
                               where pf.fileId == fileId
                               select pf).ToList();
            foreach(var pfile in projectfile)
            {
                _db.ProjectFiles.Remove(pfile);
            }
            _db.SaveChanges();
        }
        /// <summary>
        /// Going through the database to see if a file with inserted name already exists in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool DoesFileNameExistInProject(int projectId, string fileName)
        {
            var file = (from p in _db.Projects
            where p.ID == projectId
            join pf in _db.ProjectFiles on p.ID equals pf.projectId
            join f in _db.Files on pf.fileId equals f.ID
            where f.name == fileName
            select f.name).SingleOrDefault();

            if(file == fileName)
            {
                return true;
            }
            
            return false;
        }
        /// <summary>
        /// Same function as the one above, but this one checks if the extension type is the same.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public bool DoesFileNameExistInProject2(int projectId, string fileName, string extension)
        {
            var file = (from p in _db.Projects
                        where p.ID == projectId
                        join pf in _db.ProjectFiles on p.ID equals pf.projectId
                        join f in _db.Files on pf.fileId equals f.ID
                        where f.name == fileName
                        where extension == f.extension
                        select f).SingleOrDefault();

            if (file.name == fileName && extension == file.extension)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// Edits the name of a file
        /// </summary>
        /// <param name="model"></param>
        public void EditFileName(FileUpdateViewModel model)
        {
            var file = GetSingleFileById(model.ID);
            file.name = model.name;
            _db.setModified(file);
            _db.SaveChanges();
        }
        /// <summary>
        /// Getting the number of occurrences a file extension appears in the system
        /// </summary>
        /// <returns></returns>
        public int GetExtensionOccurrences (string extension)
        {
            return _db.Files.Where(x => x.extension == extension).Count();
        }
        /// <summary>
        /// Here we actually get all the extensions types and the number of extensions for each type. We use a hashMap and go through each extenisiona and count
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetStatistics()
        {
            var extensions = new ExtensionService().PopulateDropDownList();
            var stats = new Dictionary<string, int>();
            foreach(var extension in extensions)
            {
                if (extension != "md")
                {
                    stats[extension] = GetExtensionOccurrences(extension);
                }
            }
            return stats;
        }
    }
}
