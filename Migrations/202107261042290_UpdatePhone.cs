namespace ShopPhone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.Phones", new[] { "Brand_Id" });
            RenameColumn(table: "dbo.Phones", name: "Brand_Id", newName: "BrandId");
            AlterColumn("dbo.Phones", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Phones", "BrandId");
            AddForeignKey("dbo.Phones", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            DropColumn("dbo.Phones", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Phones", "BrandId", "dbo.Brands");
            DropIndex("dbo.Phones", new[] { "BrandId" });
            AlterColumn("dbo.Phones", "BrandId", c => c.Int());
            RenameColumn(table: "dbo.Phones", name: "BrandId", newName: "Brand_Id");
            CreateIndex("dbo.Phones", "Brand_Id");
            AddForeignKey("dbo.Phones", "Brand_Id", "dbo.Brands", "Id");
        }
    }
}
