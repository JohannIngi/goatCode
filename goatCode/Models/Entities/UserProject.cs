using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class UserProject
    {
        /// <summary>
        /// Parameter id is a part of UserProject to store data.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Parameter projectId is a part of UserProject to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter userId is a part of UserProject to store data.
        /// </summary>
        public string userId { get; set; }
    }
}