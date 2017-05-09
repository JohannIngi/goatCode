using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class FileUpdateViewModel
    {
        public int ID { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
    }
}