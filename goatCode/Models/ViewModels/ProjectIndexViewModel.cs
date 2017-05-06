using goatCode.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectIndexViewModel
    {
        public List<File> files { get; set; }
        public int projectId { get; set; }
    }
}