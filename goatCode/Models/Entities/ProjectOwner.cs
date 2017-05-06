using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    public class ProjectOwner
    {
        public int id { get; set; }
        public string userId { get; set; }
        public int projectId { get; set; }
    }
}