namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFieldCustomerEmailIsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CustomerEmail", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CustomerEmail", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
