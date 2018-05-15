namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime());
            AddColumn("dbo.Orders", "ShipDate", c => c.DateTime());
            AddColumn("dbo.Orders", "IsPaymented", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsShiped", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "CustomerMobile");
            DropColumn("dbo.Orders", "CustomerMessage");
            DropColumn("dbo.Orders", "CreatedDate");
            DropColumn("dbo.Orders", "CreatedBy");
            DropColumn("dbo.Orders", "PaymentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentStatus", c => c.String());
            AddColumn("dbo.Orders", "CreatedBy", c => c.String());
            AddColumn("dbo.Orders", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Orders", "CustomerMessage", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerMobile", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Orders", "IsShiped");
            DropColumn("dbo.Orders", "IsPaymented");
            DropColumn("dbo.Orders", "ShipDate");
            DropColumn("dbo.Orders", "OrderDate");
            DropColumn("dbo.Orders", "CustomerPhone");
        }
    }
}
