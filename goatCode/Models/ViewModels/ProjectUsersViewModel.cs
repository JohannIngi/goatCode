using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectUsersViewModel
    {
        /// <summary>
        /// Parameter list is a part of ProjectUserViewModel to store data.
        /// </summary>
        public List<ApplicationUser> list { get; set; }
        /// <summary>
        /// Parameter name is a part of ProjectUserViewModel to store data.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Parameter projectId is a part of ProjectUserViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
    }
}