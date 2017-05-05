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

        public List<FileViewModel> GetFilesByProjectId(int projectId)
        {
            var fileProjects = _db.Files
                .Where(x => x.projectID == projectId)
                .Select(x => x.ID)
                .ToList();

            List<FileViewModel> fileList = new List<FileViewModel>();

            var filez = _db.Files;
            foreach(var number in fileProjects)
            {
                var singleFile = (from f in filez
                                  where f.ID == number
                                  select f).SingleOrDefault();
                FileViewModel temp = new FileViewModel
                {
                    id = singleFile.ID,
                    name = singleFile.name,
                };
                fileList.Add(temp);
            }
            return fileList;
        }
        public void AddNewFile(File newFile)
        {
            _db.Files.Add(newFile);
            _db.SaveChanges();
        }
        public File GetSingleFileById(int id)
        {
            var filez = _db.Files;

            File singleFile = (from f in filez
                               where f.ID == id
                               select f).SingleOrDefault();
            
            return singleFile;
        }
        public void DeleteFile(File file)
        {
            _db.Entry(file).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
