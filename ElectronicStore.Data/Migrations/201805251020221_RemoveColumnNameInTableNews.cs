namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnNameInTableNews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.News", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.News", "Title", c => c.String());
        }
    }
}
