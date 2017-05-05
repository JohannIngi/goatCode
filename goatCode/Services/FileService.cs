using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
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

    }
}