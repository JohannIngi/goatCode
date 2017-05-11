using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static goatCode.Services.FileService;

namespace goatCode.Models.ViewModels
{
    public class StatViewModel
    {
        public ExtensionStat[] statData { get; set; }
    }
}