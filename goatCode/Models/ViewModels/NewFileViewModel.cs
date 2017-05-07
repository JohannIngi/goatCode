using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class NewFileViewModel
    {
        /// <summary>
        /// Parameter projectId is a part of NewFileViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter name is a part of NewFileViewModel to store data.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Parameter extension is a part of NewFileViewModel to store data.
        /// </summary>
        public string extension { get; set; }
    }
}