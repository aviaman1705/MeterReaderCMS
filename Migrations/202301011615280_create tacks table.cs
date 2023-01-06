namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtackstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        StreetName = c.String(),
                        NotebookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notebooks", t => t.NotebookId, cascadeDelete: true)
                .Index(t => t.NotebookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "NotebookId", "dbo.Notebooks");
            DropIndex("dbo.Tracks", new[] { "NotebookId" });
            DropTable("dbo.Tracks");
        }
    }
}
