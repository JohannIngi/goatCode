using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    /// <summary>
    /// Entity class connected to the Database.
    /// </summary>
    public class ProjectFile
    {
        /// <summary>
        /// Parameter id is a part of ProjectFile to store data.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Parameter projectId is a part of ProjectFile to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter fileId is a part of ProjectFile to store data.
        /// </summary>
        public int fileId { get; set; }
    }
}