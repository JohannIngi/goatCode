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
        public UserViewModel GetAllUsers()
        {
            var users = _db.Users.ToList();

            var viewModel = new UserViewModel()
            {

            };

            return viewModel;
        }
        public bool AddNewUser(ProjectUser newuser)
        {
       
            _db.ProjectUsers.Add(newuser);
            _db.SaveChanges();
            
            //TODO : Ef að email er nú þegar til staðar kasta villu
           // SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=hrnem.ru.is;Initial Catalog=VLN2_2017_H23;User ID=VLN2_2017_H23_usr;Password=***********";
            //con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO [Users] (Name, Email, Password) values(@name, @email ,@password)", con);

            return true;
        }

    }
}