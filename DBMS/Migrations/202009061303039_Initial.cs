using System.Data.Entity.Migrations;

namespace DBMS.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cards",
                    c => new
                    {
                        ID = c.String(false, 128),
                        ProductCodes = c.String(),
                        ProductQuantities = c.String()
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.Products",
                    c => new
                    {
                        ID = c.String(false, 128),
                        name = c.String(),
                        quantity = c.Int(false)
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        ID = c.String(false, 128),
                        username = c.String(),
                        role = c.String()
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Cards");
        }
    }
}