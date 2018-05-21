namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOrder1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Name", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "Email", c => c.String(maxLength: 256));
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "Paid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Shipped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Users", "Id");
            DropColumn("dbo.Orders", "CustomerName");
            DropColumn("dbo.Orders", "CustomerAddress");
            DropColumn("dbo.Orders", "CustomerEmail");
            DropColumn("dbo.Orders", "CustomerPhone");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "IsPaymented");
            DropColumn("dbo.Orders", "IsShiped");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsShiped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsPaymented", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String(maxLength: 256));
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "CustomerEmail", c => c.String(maxLength: 256));
            AddColumn("dbo.Orders", "CustomerAddress", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false, maxLength: 256));
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "Shipped");
            DropColumn("dbo.Orders", "Paid");
            DropColumn("dbo.Orders", "PhoneNumber");
            DropColumn("dbo.Orders", "Email");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "Name");
        }
    }
}
