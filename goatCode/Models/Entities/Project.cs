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
        /// </summary>
        [Display(Name = "Project Name")]
        [RegularExpression(@"([a-z A-Z \d]+[\w \d]*|)[a-z A-Z]+[\w \d.]*",
            ErrorMessage = "Invalid name")]
        [Required(ErrorMessage = "Project must have a name")]
        public string name { get; set; }
    }
}