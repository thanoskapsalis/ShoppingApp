using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Logic;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class UserController : Controller
    {
        User _us = new User();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        public async Task<JsonResult> CallLogin(UserModel model)
        {
           User user = new User();
           bool result= await user.LogUser(model.Username, model.Password);
           return Json(new { success = result });

        }

        public async Task<JsonResult> CallRegister(UserModel model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "https://localhost:44362/api/UserApi/RegisterUser ";
                var response = await client.PostAsync(url, data);
                if (response.StatusCode == HttpStatusCode.OK)
                    return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
    }

