namespace DBMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ImageUrl", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageUrl");
            DropColumn("dbo.Products", "Price");
        }
    }
}
