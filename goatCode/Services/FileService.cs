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
        private ApplicationDbContext _db;

        public FileService()
        {
            _db = new ApplicationDbContext();
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
        public void AddNewFile(NewFileViewModel model)
        {
            File file = new File { name = model.name, content = "", extension = model.extension };
            _db.Files.Add(file);
            _db.SaveChanges();

            _db.ProjectFiles.Add(new ProjectFile { fileId = file.ID, projectId = model.projectId });
            _db.SaveChanges();
        }
        public File GetSingleFileById(int id)
        {
            return _db.Files.Where(x => x.ID == id).SingleOrDefault();
        }
        public void DeleteAllFilesinProject(int projectId)
        {
            var files = _db.ProjectFiles.Where(x => x.projectId == projectId).ToList();
            foreach (var file in files)
            {
                _db.ProjectFiles.Remove(file);
            }
            _db.SaveChanges();
        }
        public void UpdateContent(string content)
        {
            var update = (from a in _db.Files
                          where a.content == content
                          select new { a.content }).FirstOrDefault();
        }

        

        public void DeleteFile(int id)
        {
            File file = new File { ID = id };
            _db.Files.Attach(file);
            _db.Files.Remove(file);
            _db.SaveChanges();
        }
    }

}
