using goatCode.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static goatCode.Services.FileService;

namespace goatCode.Models.ViewModels
{
    public class AdminSuperViewModel
    {
        public List<ApplicationUser> allUsers { get; set; }
        public List<Project> allProjects { get; set; }
        public List<File> allFiles { get; set; }
        public Dictionary<string, int> statData { get; set; }
    }
}