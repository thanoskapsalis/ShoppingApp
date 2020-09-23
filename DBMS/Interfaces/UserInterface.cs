using System;
using System.Threading.Tasks;

namespace DBMS.Interfaces
{
    public interface IUserInterface
    {
        Task<bool> CreateUser(string username, string password, string role);
        bool SignIn(string username, string password);
        void SignOut();
        string GetUserRole(string username);
        string GetUserId(string username);
        string GetUsername(string id);
    }
}