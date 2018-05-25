namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTagFiedld : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "HomeFlag");
            DropColumn("dbo.News", "HotFlag");
            DropColumn("dbo.Products", "HomeFlag");
            DropColumn("dbo.Products", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Tags", c => c.String());
            AddColumn("dbo.Products", "HomeFlag", c => c.Boolean());
            AddColumn("dbo.News", "HotFlag", c => c.Boolean());
            AddColumn("dbo.News", "HomeFlag", c => c.Boolean());
        }
    }
}
