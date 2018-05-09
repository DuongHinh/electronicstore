namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldFaxForTableContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Fax", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Fax");
        }
    }
}
