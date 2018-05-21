namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Alias", c => c.String());
            AddColumn("dbo.Brands", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Status");
            DropColumn("dbo.Brands", "Alias");
        }
    }
}
