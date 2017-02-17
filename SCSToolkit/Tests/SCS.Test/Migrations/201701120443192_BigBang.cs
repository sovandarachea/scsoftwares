namespace SCS.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BigBang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseUrl = c.String(),
                        UrlsJson = c.String(),
                        UpdateOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileLinks");
        }
    }
}
