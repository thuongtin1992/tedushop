namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewCountToPageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "ViewCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "ViewCount");
        }
    }
}
