using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Logic;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly User _us = new User();

        [HttpPost]
        public async Task<HttpResponseMessage> RegisterUser(UserModel model)
        {
            var result = await _us.MakeUser(model.Username, model.Password, model.Role);
            return result
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> LoginUser(UserModel model)
        {
            var result = await _us.LogUser(model.Username, model.Password);
            return result
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        [HttpPost]
        public string GetUserRole([FromBody] string username)
        {
            return _us.GetUserRole(username);
        }
    }
}