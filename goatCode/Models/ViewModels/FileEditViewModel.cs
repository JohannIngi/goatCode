using goatCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatCode.Models.ViewModels
{
    public class FileEditViewModel
    {
        private ExtensionService  utservice = new ExtensionService();
        /// <summary>
        /// Parameter ID is a part of FileEditViewModel to store data.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Parameter projectId is a part of FileEditViewModel to store data.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Parameter name is a part of FileEditViewModel to store data.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Parameter content is a part of FileEditViewModel to store data.
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// Parameter extension is a part of FileEditViewModel to store data.
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// Parameter extensionSetting is a part of FileEditViewModel to store data.
        /// </summary>
        public string extensionSetting()
        {
            return utservice.GetAceSettingsValueForExtension(extension);
        }

    }
}