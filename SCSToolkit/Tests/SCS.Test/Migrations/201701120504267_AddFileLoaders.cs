namespace SCS.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileLoaders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileLoaders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        Contents = c.Binary(),
                        UpdateOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileLoaders");
        }
    }
}
