using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class FileUpdateViewModel
    {
        /// <summary>
        /// Parameter ID is a part of FileUpdateViewModel to store data.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Parameter projectId is a part of FileUpdateViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter name is a part of FileUpdateViewModel to store data.
        /// </summary>
        public string name { get; set; }
    }
}