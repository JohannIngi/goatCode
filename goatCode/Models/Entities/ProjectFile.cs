﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.Entities
{
    public class ProjectFile
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int fileId { get; set; }
    }
}