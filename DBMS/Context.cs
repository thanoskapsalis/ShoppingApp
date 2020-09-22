using System.Data.Entity;
using DBMS.Models;

namespace DBMS
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}