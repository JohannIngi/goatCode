using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public List<FileViewModel> files { get; set; }
    }
}