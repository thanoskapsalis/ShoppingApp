using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DBMS.Models;
using Logic;
using Newtonsoft.Json;
using ShoppingApp.Models;
using User = Logic.User;

namespace ShoppingApp.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            var product = new ProductClass();
            List<Product> allCatalog = null;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "=" + User.Identity.Name;
                var result = client.UploadString("https://localhost:44362/api/UserApi/GetUserRole", "POST", data);
                product.UserRole = result;
            }

            using (var client = new HttpClient())
            {
                var task = client.GetAsync("https://localhost:44362/api/ShopApi/GetProducts").ContinueWith(
                    taskwithresponse =>
                    {
                        var response = taskwithresponse.Result;
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();
                        allCatalog = JsonConvert.DeserializeObject<List<Product>>(jsonString.Result);
                    }
                );
                task.Wait();
                product.Products = allCatalog;
            }


            return View(product);
        }

        public async Task<JsonResult> Order(List<string> model)
        {
            var cart = new Dictionary<string, int>();
            var counter = 0;
            while (counter < model.Count)
                try
                {
                    cart.Add(model[counter], int.Parse(model[counter + 1]));
                    counter = counter + 2;
                }
                catch (Exception e)
                {
                    cart.Add(model[counter], 0);
                    counter = counter + 2;
                }

            cart.Add(User.Identity.Name, -1);
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cart);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                const string url = "https://localhost:44362/api/ShopApi/PlaceOrder";
                var response = await client.PostAsync(url, data);
                if (response.StatusCode != HttpStatusCode.OK) return Json(new {success = true});
                var shop = new Shop();
                var price = shop.CalculatePrice(cart);
                return Json(new {success = true, price});
            }
        }

        

        public ActionResult MyCart()
        {
            var sh = new Shop();
            var us = new User();
            var product = new ProductClass();
            var allCards = new List<Card>();
            using (var client = new HttpClient())
            {
                var task = client.GetAsync("https://localhost:44362/api/ShopApi/GetAllCards").ContinueWith(
                    taskwithresponse =>
                    {
                        var response = taskwithresponse.Result;
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();
                        allCards = JsonConvert.DeserializeObject<List<Card>>(jsonString.Result);
                    }
                );
                task.Wait();
                product.Cards = allCards;
            }

            var userid = us.GetUserId(User.Identity.Name);
            var nameList = new List<Card>();
            foreach (var item in allCards)
                if (item.Uploader == userid)
                {
                    var id = item.ProductCodes.Split(',');
                    var temp = new StringBuilder();
                    foreach (var single in id)
                    {
                        if (single == "")
                            continue;
                        temp.Append("," + sh.GetProduct(single).Name);
                    }

                    var cr = new Card
                    {
                        Id = item.Id, ProductQuantities = item.ProductQuantities, ProductCodes = temp.ToString(),
                        Uploader = item.Uploader
                    };
                    nameList.Add(cr);
                }

            product.Cards = nameList;
            return View(product);
        }
    }
}