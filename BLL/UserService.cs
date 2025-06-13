using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService
    {
        private UserManagement userManag = new UserManagement();

        public User? ValidateLogin(string username, string password)
        {
            return userManag.GetUser(username, password);
        }
    }
}


