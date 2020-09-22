using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Functions;
using DBMS.Interfaces;
using DBMS.Models;

namespace Logic
{
    public class Shop
    {
        private readonly IProductInterface _product = new ProductFunctions();
        private readonly IUserInterface _user = new UserFunctions();

        public async Task<bool> NewProduct(string modelProductName, string modelProductPrice, string modelProductImage,
            string modelProductQuantity)
        {
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = modelProductName,
                    Price = int.Parse(modelProductPrice),
                    ImageUrl = modelProductImage,
                    Quantity = int.Parse(modelProductQuantity)
                };
                await _product.NewProduct(product);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Product>> LoadAllProducts()
        {
            try
            {
                return await _product.LoadAllProducts();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PlaceOrder(Dictionary<string, int> order)
        {
            var arrayOfAllKeys = order.Keys.ToArray();
            var arrayOfAllValues = order.Values.ToArray();
            var productId = new StringBuilder();
            var productQuantity = new StringBuilder();
            for (var i = 0; i < arrayOfAllKeys.Length; i++)
            {
                if (arrayOfAllValues[i] <= 0)
                    continue;
                productId.Append("," + _product.GetProductByName(arrayOfAllKeys[i]));
                productQuantity.Append("," + arrayOfAllValues[i]);

                //Before we left that loop we have to remove the products that the user bought from the stock;
                var product = _product.GetProductById(_product.GetProductByName(arrayOfAllKeys[i]));
                product.Quantity = product.Quantity - arrayOfAllValues[i];
                await _product.NewProduct(product);
            }

            var uploader = order.FirstOrDefault(x => x.Value == -1).Key;
            var orderCard = new Card
            {
                ProductCodes = productId.ToString(),
                ProductQuantities = productQuantity.ToString(),
                Id = Guid.NewGuid().ToString(),
                Uploader = _user.GetUserId(uploader)
            };

            try
            {
                _product.PlaceOrder(orderCard);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CalculatePrice(Dictionary<string, int> order)
        {
            var price = 0;
            var arrayOfAllKeys = order.Keys.ToArray();
            var arrayOfAllValues = order.Values.ToArray();
            var productId = new StringBuilder();
            var productQuantity = new StringBuilder();
            for (var i = 0; i < arrayOfAllKeys.Length; i++)
            {
                if (arrayOfAllValues[i] <= 0)
                    continue;
                productId.Append("," + _product.GetProductByName(arrayOfAllKeys[i]));
                productQuantity.Append("," + arrayOfAllValues[i]);

                //Before we left that loop we have to remove the products that the user bought from the stock;
                var product = _product.GetProductById(_product.GetProductByName(arrayOfAllKeys[i]));
                price = price + product.Price * arrayOfAllValues[i];
            }

            return price;
        }

        public List<Card> GetAllCards()
        {
            return _product.GetAllCards();
        }

        public List<Card> GetUserCards(string username)
        {
            var user = _user.GetUserId(username);
            return _product.GetUserCards(user);
        }

        public Product GetProduct(string id)
        {
            return _product.GetProductById(id);
        }

        public bool DeleteCard(string id)
        {
            try
            {
                _product.DeleteCard(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}