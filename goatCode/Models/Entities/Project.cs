using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }
        [Display(Name = "Project Name")]
        [RegularExpression(@"([a-z A-Z \d]+[\w \d]*|)[a-z A-Z]+[\w \d.]*",
            ErrorMessage = "Invalid name")]
        public string name { get; set; }


    }
}