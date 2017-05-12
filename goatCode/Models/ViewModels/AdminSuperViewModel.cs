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
        /// <summary>
        /// Parameter allUsers is a part of AdminSuperViewModel to store data.
        /// </summary>
        public List<ApplicationUser> allUsers { get; set; }
        /// <summary>
        /// Parameter allProjects is a part of AdminSuperViewModel to store data.
        /// </summary>
        public List<Project> allProjects { get; set; }
        /// <summary>
        /// Parameter allFiles is a part of AdminSuperViewModel to store data.
        /// </summary>
        public List<File> allFiles { get; set; }
        /// <summary>
        /// Parameter statData is a part of AdminSuperViewModel to store data.
        /// </summary>
        public Dictionary<string, int> statData { get; set; }
    }
}