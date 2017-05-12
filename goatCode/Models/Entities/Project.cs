using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Models.Entities
{
    /// <summary>
    /// Entity class connected to the Database.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Parameter ID is a part of Project to store data.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Parameter name is a part of Project to store data.
        /// Regular expression to check if name is legal.
        /// </summary>
        
        [Display(Name = "Project Name")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,30}$",
            ErrorMessage = "Invalid name, must be between 1-30 characters and only letters and numbers.")]
        [Required(ErrorMessage = "Project must have a name")]
        public string name { get; set; }
    }
}