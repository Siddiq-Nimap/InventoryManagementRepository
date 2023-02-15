namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatagoryName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CatagoryLists",
                c => new
                    {
                        PId = c.Int(nullable: false),
                        CId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PId, t.CId })
                .ForeignKey("dbo.Catagories", t => t.CId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.PId, cascadeDelete: true)
                .Index(t => t.PId)
                .Index(t => t.CId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGenericName = c.String(),
                        ProductDescription = c.String(),
                        ProductTitle = c.String(),
                        ProductWeight = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ImagePath = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logins", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Email_Id = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CatagoryLists", "PId", "dbo.Products");
            DropForeignKey("dbo.Products", "UserID", "dbo.Logins");
            DropForeignKey("dbo.CatagoryLists", "CId", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "UserID" });
            DropIndex("dbo.CatagoryLists", new[] { "CId" });
            DropIndex("dbo.CatagoryLists", new[] { "PId" });
            DropTable("dbo.Logins");
            DropTable("dbo.Products");
            DropTable("dbo.CatagoryLists");
            DropTable("dbo.Catagories");
        }
    }
}
