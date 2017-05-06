using goatCode.Models;
using goatCode.Models.Entities;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace goatCode.Services
{
    public class UserService
    {
        private ApplicationDbContext _db;

        public UserService()
        {
            _db = new ApplicationDbContext();
        }
        public bool DoesUserExist(string email)
        {
            foreach(var user in _db.Users)
            {
                if(user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}