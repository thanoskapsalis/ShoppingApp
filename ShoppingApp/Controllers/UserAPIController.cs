using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Logic;
using Microsoft.Ajax.Utilities;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class UserApiController : ApiController
    {
        readonly User _us = new User();
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> RegisterUser(UserModel model)
        {
           var result= await _us.MakeUser(model.Username, model.Password, model.Role);
           if(result)
               return new HttpResponseMessage(HttpStatusCode.OK);
           return  new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> LoginUser(UserModel model)
        {
            bool result = await _us.LogUser(model.Username, model.Password);
            if(result)
                return  new HttpResponseMessage(HttpStatusCode.OK);
            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        [System.Web.Http.HttpPost]
        public string GetUserRole([FromBody] string username)
        {
            return _us.GetUserRole(username);
        }
    }
}
