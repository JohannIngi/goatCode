using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class hkhgjhgjyg
    {
        public IEnumerable<FileViewModel> lst { get; set; }
        public int projectId { get; set; }

        public hkhgjhgjyg(FileService service, int projectId)
        {
            lst = service.GetFilesByProjectId(projectId);
            this.projectId = projectId;
        }

        public bool isValid()
        {
            return lst != null;
        }
    }
}