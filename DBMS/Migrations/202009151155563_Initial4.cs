using System.Data.Entity.Migrations;

namespace DBMS.Migrations
{
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "Uploader", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Cards", "Uploader");
        }
    }
}