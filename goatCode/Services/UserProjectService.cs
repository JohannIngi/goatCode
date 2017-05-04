using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goatCode.Models;
using goatCode.Models.ViewModels;

namespace goatCode.Services
{
    public class UserProjectService
    {

        private ApplicationDbContext _db;

        public UserProjectService()
        {
            _db = new ApplicationDbContext();
        }

        public List<ProjectViewModel> GetProjectByUser(int userID)
        {


            return null;
        }
      

    }




}