using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    /// <summary>
    /// Entity class connected to the Database.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Parameter ID is a part of File to store data.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Parameter name is a part of File to store data.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Parameter extension is a part of File to store data.
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// Parameter content is a part of File to store data.
        /// </summary>
        public string content { get; set; }

    }
}