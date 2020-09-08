
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DBMS.Interfaces;
using DBMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using  Microsoft.Owin.Host.SystemWeb;


namespace DBMS.Functions
{
    public class UserFunctions : UserInterface
    {
        public async Task CreateUser(string username, string password, string role)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser() { UserName =username };
            IdentityResult result = manager.Create(user, password);

            if (result.Succeeded)
            {
                using (var db = new Context())
                {
                    db.Users.Add(new User()
                    {
                        ID = Guid.NewGuid().ToString(), username = username, role = role

                    });
                    await db.SaveChangesAsync();
                }
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);

            }
        }

        public async Task<bool> SignIn(string username,string password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(username,password);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SignOut(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            
        }
    }
}
