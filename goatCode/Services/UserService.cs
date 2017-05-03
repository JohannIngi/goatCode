using goatCode.Models;
using goatCode.Models.ViewModels;
using System;
using System.Collections.Generic;
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
        public UserViewModel GetAllUsers()
        {
            var users = _db.Users.ToList();

            var viewModel = new UserViewModel()
            {

            };

            return viewModel;
        }

    }
}