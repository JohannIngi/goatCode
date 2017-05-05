using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }
        [Display(Name = "Project Name")]
        public string name { get; set; }


    }
}