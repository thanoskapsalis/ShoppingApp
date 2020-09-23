using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DBMS.Interfaces;
using DBMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace DBMS.Functions
{
    public class UserFunctions : IUserInterface
    {
        public async Task<bool> CreateUser(string username, string password, string role)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser {UserName = username};
            var result = manager.Create(user, password);

            if (result.Succeeded)
            {
                using (var db = new Context())
                {
                    db.Users.Add(new User
                    {
                        Id = Guid.NewGuid().ToString(), Username = username, Role = role
                    });
                    await db.SaveChangesAsync();
                }

                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                return true;
            }

            return false;
        }

        public bool SignIn(string username, string password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(username, password);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false}, userIdentity);
                return true;
            }

            return false;
        }

        

        public void SignOut()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }

        public string GetUserRole(string username)
        {
            string role;
            using (var db = new Context())
            {
                role = db.Users.First(sh => sh.Username == username).Role;
            }

            return role;
        }

        public string GetUserId(string username)
        {
            using (var db = new Context())
            {
                return db.Users.First(sh => sh.Username == username).Id;
            }
        }

        public string GetUsername(string id)
        {
            using (var db = new Context ())
            {
                return db.Users.First(sh => sh.Id == id).Username;
            }
        }
    }
}