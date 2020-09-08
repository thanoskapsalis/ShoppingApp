using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Interfaces
{
    public interface UserInterface
    {
        Task CreateUser(string username, string password, string role);
        Task<bool> SignIn(string username, string password);
        void SignOut(object sender, EventArgs e);
    }
}
