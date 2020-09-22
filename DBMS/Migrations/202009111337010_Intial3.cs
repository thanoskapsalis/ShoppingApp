using System.Data.Entity.Migrations;

namespace DBMS.Migrations
{
    public partial class Intial3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.Int(false));
        }
    }
}