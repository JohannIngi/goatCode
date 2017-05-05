using goatCode.Models;
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

        public List<FileViewModel> GetFilesByProject(int projectId)
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
                    type = "." + "máni"
                };
                fileList.Add(temp);
            }
            return fileList;
        }

    }
}