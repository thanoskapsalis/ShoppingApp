using System.Threading.Tasks;
using DBMS.Functions;
using DBMS.Interfaces;

namespace Logic
{
    public class User
    {
        private readonly IUserInterface _userInterface = new UserFunctions();

        public async Task<bool> MakeUser(string username, string password, string role)
        {
            var result = await _userInterface.CreateUser(username, password, role);
            return result;
        }


        public async Task<bool> LogUser(string modelUsername, string modelPassword)
        {
            var result = _userInterface.SignIn(modelUsername, modelPassword);
            return result;
        }

        public string GetUserRole(string username)
        {
            return _userInterface.GetUserRole(username);
        }

        public string GetUserId(string username)
        {
            return _userInterface.GetUserId(username);
        }

        public void SignOut ()
        {
            _userInterface.SignOut();
        }

        public string GetUsername(string id)
        {
            return _userInterface.GetUsername(id);
        }
    }
}