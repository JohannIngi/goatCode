using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatCode.Models.ViewModels
{
    public class FileUpdateViewModel
    {
        public int ID { get; set; }
        public int projectId { get; set; }
        [Display(Name = "File Name")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,30}$",
                     ErrorMessage = "Invalid name, must be between 1-30 characters and only letters and numbers.")]
        [Required(ErrorMessage = "Must have a name")]
        public string name { get; set; }
    }
}