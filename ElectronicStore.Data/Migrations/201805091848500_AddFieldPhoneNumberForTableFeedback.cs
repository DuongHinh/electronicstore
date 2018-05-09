namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldPhoneNumberForTableFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.Feedbacks", "PhoneNumber", c => c.String(maxLength: 50));
            DropColumn("dbo.Contacts", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Phone", c => c.String(maxLength: 50));
            DropColumn("dbo.Feedbacks", "PhoneNumber");
            DropColumn("dbo.Contacts", "PhoneNumber");
        }
    }
}
