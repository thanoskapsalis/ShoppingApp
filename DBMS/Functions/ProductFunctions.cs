using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using DBMS.Interfaces;
using DBMS.Models;

namespace DBMS.Functions
{
    public class ProductFunctions : IProductInterface
    {
        public async Task NewProduct(Product prod)
        {
            using (var db = new Context())
            {
                db.Products.AddOrUpdate(prod);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> LoadAllProducts()
        {
            using (var db = new Context())
            {
                return await db.Products.ToListAsync();
            }
        }

        public string GetProductByName(string name)
        {
            using (var db = new Context())
            {
                return db.Products.First(sh => sh.Name == name).Id;
            }
        }

        public Product GetProductById(string id)
        {
            using (var db = new Context())
            {
                return db.Products.First(sh => sh.Id == id);
            }
        }

        public void PlaceOrder(Card orderCard)
        {
            using (var db = new Context())
            {
                db.Cards.Add(orderCard);
                db.SaveChanges();
            }
        }

        public List<Card> GetUserCards(string id)
        {
            using (var db = new Context())
            {
                return db.Cards.Where(sh => sh.Uploader == id).ToList();
            }
        }

        public List<Card> GetAllCards()
        {
            using (var db = new Context())
            {
                return db.Cards.ToList();
            }
        }

        public void DeleteCard(string id)
        {
            using (var db = new Context())
            {
                var pr = db.Cards.First(sh => sh.Id == id);
                db.Cards.Remove(pr);
                db.SaveChanges();
            }
        }
    }
}