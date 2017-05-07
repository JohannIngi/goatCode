using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    /// <summary>
    /// Entity class connected to the Database.
    /// </summary>
    public class ProjectOwner
    {
        /// <summary>
        /// Parameter id is a part of ProjectOwner to store data.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Parameter userId is a part of ProjectOwner to store data.
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// Parameter projectId is a part of ProjectOwner to store data.
        /// </summary>
        public int projectId { get; set; }
    }
}