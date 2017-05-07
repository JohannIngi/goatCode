using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class FileService
    {
        /// <summary>
        /// Instance of database.
        /// </summary>

        static Dictionary<string, string> aceMap = new Dictionary<string, string>
        {
            ["c"] = "c_cpp",
            ["cpp"] = "c_cpp",
            ["java"] = "java",
            ["py"] = "python"
        };


        private ApplicationDbContext _db;

        public FileService()
        {
            _db = new ApplicationDbContext();
        }

        /// <summary>
        /// If projectID is in Projects table. It searches for the same projectID in ProjectFiles table.
        /// When the projectID is found in ProjectFiles we take the fileID from the same ProjectFilesID.
        /// </summary>
        /// <param name="id">To match Projects.ID with the value of this id</param>
        /// <returns>Returns a list of all files in this projectID.</returns>
        public string getAceSettingsValueForExtension(string extension)
        {
            if (aceMap.ContainsKey(extension))
            {
                return aceMap[extension];
            }
            return "txt";
        }

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

        public void AddNewFile(NewFileViewModel model)
        {
            File file = new File { name = model.name, content = "", extension = model.extension };
            _db.Files.Add(file);
            _db.SaveChanges();

            _db.ProjectFiles.Add(new ProjectFile { fileId = file.ID, projectId = model.projectId });
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

        public void DeleteAllFilesinProject(int projectId)
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

        public void UpdateFile(File file)
        {
            //TODO : Þetta virkar ekki þarf að skoða betur seinna.
            _db.Entry(file).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// Finds a file with some ID and removes it from Files table.
        /// </summary>
        /// <param name="id">Lets the ID in file be the same as the parameter</param>
        public void DeleteFile(int id)
        {
            File file = new File { ID = id };
            _db.Files.Attach(file);
            _db.Files.Remove(file);
            _db.SaveChanges();
        }
    }

}
