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

namespace ShoppingApp.Controllers
{
    public class AdminController : Controller
    {
        private string role;
        private readonly Shop sh = new Shop();

        // GET: Admin
        public async Task<ActionResult> NewProduct()
        {
            var product = new ProductClass {UserRole = role};
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "=" + User.Identity.Name;
                var result = client.UploadString("https://localhost:44362/api/UserApi/GetUserRole", "POST", data);
                product.UserRole = result;
            }

            return View(product);
        }

        public async Task<JsonResult> AddProduct(ProductClass model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "https://localhost:44362/api/ShopApi/NewProduct";
                var response = await client.PostAsync(url, data);
                if (response.StatusCode == HttpStatusCode.OK)
                    return Json(new {success = true});
                return Json(new {success = false});
            }
        }

        public ActionResult AllCarts()
        {
            var product = new ProductClass {UserRole = role};
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "=" + User.Identity.Name;
                var result = client.UploadString("https://localhost:44362/api/UserApi/GetUserRole", "POST", data);
                product.UserRole = result;
            }

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
                var us = new Logic.User();
                foreach (var items in allCards)
                {
                    items.Uploader=us.GetUsername(items.Uploader);
                }
                product.Cards = allCards;
            }

            var nameList = new List<Card>();
            foreach (var item in allCards)
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

        public ActionResult Delete(string id)
        {
            var sh = new Shop();
            sh.DeleteCard(id);
            return Redirect("/Shop/Index");
        }
    }
}