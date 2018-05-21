namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTableMenu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "TypeId", "dbo.MenuTypes");
            DropIndex("dbo.Menus", new[] { "TypeId" });
            DropTable("dbo.Menus");
            DropTable("dbo.MenuTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MenuTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Link = c.String(),
                        Order = c.Int(nullable: false),
                        Target = c.String(),
                        TypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Menus", "TypeId");
            AddForeignKey("dbo.Menus", "TypeId", "dbo.MenuTypes", "Id", cascadeDelete: true);
        }
    }
}
