using goatCode.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectIndexViewModel
    {
        /// <summary>
        /// Parameter ID is a list of ProjectIndexViewModel to store data.
        /// </summary>
        public List<File> files { get; set; }
        /// <summary>
        /// Parameter projectId is a part of ProjectIndexViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter name is a part of ProjectIndexViewModel to store data.
        /// </summary>
        public string name { get; set; }
    }
}