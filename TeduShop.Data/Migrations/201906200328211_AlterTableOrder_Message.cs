namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableOrder_Message : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
