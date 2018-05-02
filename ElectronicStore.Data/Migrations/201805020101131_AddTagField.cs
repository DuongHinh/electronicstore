namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Tags", c => c.String());
            AddColumn("dbo.Products", "Tags", c => c.String());
            AlterColumn("dbo.Tags", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Type", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Products", "Tags");
            DropColumn("dbo.News", "Tags");
        }
    }
}
