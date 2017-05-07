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
        [AllowHtml]
        public string name { get; set; }


    }
}