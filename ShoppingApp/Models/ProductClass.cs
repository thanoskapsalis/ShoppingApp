using System.Collections.Generic;
using DBMS.Models;

namespace ShoppingApp.Models
{
    public class ProductClass
    {
        public string UserRole { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductImage { get; set; }
        public List<Product> Products { get; set; }
        public List<Card> Cards { get; set; }
    }
}