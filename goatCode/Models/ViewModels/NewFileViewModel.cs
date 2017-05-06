using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class NewFileViewModel
    {
        public int projectId { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }
}