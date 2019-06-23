namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Quantitty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Quantitty", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Quantity");
        }
    }
}
