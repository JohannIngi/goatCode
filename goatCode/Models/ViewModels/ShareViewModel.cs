using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ShareViewModel
    {
        /// <summary>
        /// Parameter email is a part of ShareViewModel to store data.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        /// <summary>
        /// Parameter projectId is a part of ShareViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
    }
}