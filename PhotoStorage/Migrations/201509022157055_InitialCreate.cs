namespace PhotoStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        GalleryId = c.Int(nullable: false, identity: true),
                        GalleryName = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        GalleryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Galleries", t => t.GalleryId, cascadeDelete: true)
                .Index(t => t.GalleryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "GalleryId", "dbo.Galleries");
            DropIndex("dbo.Photos", new[] { "GalleryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
            DropTable("dbo.Galleries");
        }
    }
}
