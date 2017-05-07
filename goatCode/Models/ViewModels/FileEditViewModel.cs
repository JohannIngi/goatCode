﻿using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatCode.Models.ViewModels
{
    public class FileEditViewModel
    {
        private FileService _fservice = new FileService();
        public int ID { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string extension { get; set; }
        public string extensionSetting()
        {
            return _fservice.getAceSettingsValueForExtension(extension);
        }

    }
}