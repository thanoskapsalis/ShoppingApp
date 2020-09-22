using System.Collections.Generic;
using System.Threading.Tasks;
using DBMS.Models;

namespace DBMS.Interfaces
{
    public interface IProductInterface
    {
        Task NewProduct(Product prod);
        Task<List<Product>> LoadAllProducts();
        string GetProductByName(string name);
        void PlaceOrder(Card orderCard);
        Product GetProductById(string id);
        List<Card> GetUserCards(string id);
        List<Card> GetAllCards();
        void DeleteCard(string id);
    }
}