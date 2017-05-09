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
        /// 
        [Required (ErrorMessage = "Must have a name")]
        [Display(Name = "File Name")]
        public string name { get; set; }
        /// <summary>
        /// Parameter extension is a part of NewFileViewModel to store data.
        /// </summary>
        public string extension { get; set; }
        public IEnumerable<SelectListItem> extensionTypes { get; set; }

    }
}