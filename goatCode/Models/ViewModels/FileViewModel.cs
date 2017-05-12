using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace goatCode.Models.ViewModels
{
    public class FileViewModel
    {
        /// <summary>
        /// Parameter id is a part of FileViewModel to store data.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Parameter projectId is a part of FileViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter name is a part of FileViewModel to store data.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Parameter extension is a part of FileViewModel to store data.
        /// </summary>
        public string extension { get; set; }


    }
}