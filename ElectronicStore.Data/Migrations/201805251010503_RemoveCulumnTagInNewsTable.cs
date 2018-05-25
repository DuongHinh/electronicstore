namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCulumnTagInNewsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "Tags");
            DropColumn("dbo.NewsCategories", "DisplayOrder");
            DropColumn("dbo.NewsCategories", "HomeFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsCategories", "HomeFlag", c => c.Boolean());
            AddColumn("dbo.NewsCategories", "DisplayOrder", c => c.Int());
            AddColumn("dbo.News", "Tags", c => c.String());
        }
    }
}
