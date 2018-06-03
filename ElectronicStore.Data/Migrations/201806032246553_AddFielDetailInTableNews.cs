namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFielDetailInTableNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Detail");
        }
    }
}
