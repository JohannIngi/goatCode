using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Models.ViewModels
{
    public class NewFileViewModel
    {
        /// <summary>
        /// Parameter projectId is a part of NewFileViewModel to store data.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Parameter name is a part of NewFileViewModel to store data.
        /// </summary>
        [Display(Name = "File Name")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,30}$",
            ErrorMessage = "Invalid name, must be between 1-30 characters and only letters and numbers.")]
        [Required (ErrorMessage = "Must have a name")]
        public string name { get; set; }
        /// <summary>
        /// Parameter extension is a part of NewFileViewModel to store data.
        /// </summary>
        public string extension { get; set; }
        public IEnumerable<SelectListItem> extensionTypes { get; set; }

    }
}