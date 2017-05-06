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
        public void AddNewFile(File newFile)
        {
            _db.Files.Add(newFile);
            _db.SaveChanges();
        }
        public File GetSingleFileById(int id)
        {
            return _db.Files.Where(x => x.ID == id).SingleOrDefault();
        }
        public void DeleteFile(File file)
        {
            _db.Entry(file).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
