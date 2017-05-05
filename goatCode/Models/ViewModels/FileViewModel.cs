using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class FileViewModel
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
        public int extensionId { get; set; }


    }
}