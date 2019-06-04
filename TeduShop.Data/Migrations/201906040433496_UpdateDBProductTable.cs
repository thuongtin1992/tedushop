namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDBProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
            AddColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Products", "MoreImage");
            DropColumn("dbo.Products", "Promotion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Promotion", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "MoreImage", c => c.String(storeType: "xml"));
            DropColumn("dbo.Products", "PromotionPrice");
            DropColumn("dbo.Products", "MoreImages");
        }
    }
}
