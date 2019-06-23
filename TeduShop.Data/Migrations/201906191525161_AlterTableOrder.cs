namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
