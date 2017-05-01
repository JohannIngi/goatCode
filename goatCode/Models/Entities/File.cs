using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    public class File
    {
        public int ID { get; set; }
        public int projectID { get; set; }
        public string name { get; set; }
        public string extension { get; set; }

    }
}