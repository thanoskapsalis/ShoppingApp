using System.Data.Entity.Migrations;

namespace DBMS.Migrations
{
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Int(false));
            AddColumn("dbo.Products", "ImageUrl", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "ImageUrl");
            DropColumn("dbo.Products", "Price");
        }
    }
}