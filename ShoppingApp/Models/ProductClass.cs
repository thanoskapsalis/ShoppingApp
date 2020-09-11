using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class ProductClass
    {
        public string UserRole { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductImage { get; set; }
    }
}