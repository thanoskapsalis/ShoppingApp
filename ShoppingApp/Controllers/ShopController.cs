using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            ProductClass product = new ProductClass();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "="+User.Identity.Name;
                var result = client.UploadString("https://localhost:44362/api/UserApi/GetUserRole", "POST", data);
                product.UserRole = result;
            }
           
            return View(product);
            
        }
    }
}