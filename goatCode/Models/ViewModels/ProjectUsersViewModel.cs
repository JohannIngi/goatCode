using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectUsersViewModel
    {
        public List<ApplicationUser> list { get; set; }
        public string name { get; set; }
        public int projectId { get; set; }
    }
}