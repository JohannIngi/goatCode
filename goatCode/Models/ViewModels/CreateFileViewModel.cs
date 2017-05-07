using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class CreateFileViewModel
    {
        // TODO: Annotations
        public string name { get; set; }
        public string extension { get; set; }
        public int projectId { get; set; }
    }
}