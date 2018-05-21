namespace ElectronicStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableProductAndAddTableBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.News", "Title", c => c.String());
            AddColumn("dbo.Products", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "BrandId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            DropColumn("dbo.Abouts", "Detail");
            DropColumn("dbo.News", "Detail");
            DropColumn("dbo.Products", "Warranty");
            DropColumn("dbo.Products", "Detail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Detail", c => c.String());
            AddColumn("dbo.Products", "Warranty", c => c.Int());
            AddColumn("dbo.News", "Detail", c => c.String());
            AddColumn("dbo.Abouts", "Detail", c => c.String());
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropColumn("dbo.Products", "BrandId");
            DropColumn("dbo.News", "Title");
            DropTable("dbo.Brands");
        }
    }
}
