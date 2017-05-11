using goatCode.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectViewModel
    {
        /// <summary>
        /// Parameter ID is a part of ProjectViewModel to store data.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Parameter name is a part of ProjectViewModel to store data.
        /// </summary>
        
        public string name { get; set; }
        /// <summary>
        /// Parameter files is a list of ProjectViewModel to store data.
        /// </summary>
        
        public string projectOwner { get; set; }
    }
}