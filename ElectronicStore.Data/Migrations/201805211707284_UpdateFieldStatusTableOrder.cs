namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFieldStatusTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ShipStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Paid");
            DropColumn("dbo.Orders", "Shipped");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Shipped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Paid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "ShipStatus");
            DropColumn("dbo.Orders", "PaymentStatus");
        }
    }
}
