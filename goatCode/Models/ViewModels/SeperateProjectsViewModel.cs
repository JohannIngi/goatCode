using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goatCode.Models.Entities;
using goatCode.Services;

namespace goatCode.Models.ViewModels
{
    public class SeperateProjectsViewModel
    {
        private ProjectService _pserv = new ProjectService();
        
        public List<Project> OwnerList(string userName)
        {
            return _pserv.GetProjectsOwnedByUser(userName);
        }

        //public List<Project> NowOwnerList()
        //{
        //    return _pserv.
        //}

    }
}