using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Functions;
using DBMS.Interfaces;

namespace Logic
{
    public class User
    {
        readonly IUserInterface _userInterface = new UserFunctions();

        public async  Task<bool> MakeUser(string username,string password, string role)
        {
           bool result= await _userInterface.CreateUser(username, password, role);
           return result;
        }


        public async Task<bool> LogUser(string modelUsername, string modelPassword)
        {
           var result =  _userInterface.SignIn(modelUsername, modelPassword);
           return result;
        }

        public string GetUserRole(string username)
        {
            return _userInterface.GetUserRole(username);
        }
    }
}
