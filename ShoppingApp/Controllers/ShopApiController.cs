using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DBMS.Models;
using Logic;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class ShopApiController : ApiController
    {
        private readonly Shop sh = new Shop();

        public async Task<HttpResponseMessage> NewProduct(ProductClass model)
        {
            var result = await sh.NewProduct(model.ProductName, model.ProductPrice, model.ProductImage,
                model.ProductQuantity);
            return result
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await sh.LoadAllProducts();
        }

        public Product GetProductById(string id)
        {
            return sh.GetProduct(id);
        }

        public async Task<HttpResponseMessage> PlaceOrder(Dictionary<string, int> order)
        {
            var result = await sh.PlaceOrder(order);
            return result
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        public List<Card> GetAllCards()
        {
            return sh.GetAllCards();
        }

        public List<Card> GetUserCards(string username)
        {
            return sh.GetUserCards(username);
        }

        public HttpResponseMessage DeleteCard(string id)
        {
            var result = sh.DeleteCard(id);
            return result
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}