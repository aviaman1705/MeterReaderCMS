namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removemeterreaderstable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeterReaders", "NotebookId", "dbo.Notebooks");
            DropForeignKey("dbo.MeterReaders", "UserId", "dbo.Users");
            DropIndex("dbo.MeterReaders", new[] { "NotebookId" });
            DropIndex("dbo.MeterReaders", new[] { "UserId" });
            DropTable("dbo.MeterReaders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MeterReaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Called = c.Int(nullable: false),
                        UnCalled = c.Int(nullable: false),
                        Left = c.Int(nullable: false),
                        NotebookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MeterReaders", "UserId");
            CreateIndex("dbo.MeterReaders", "NotebookId");
            AddForeignKey("dbo.MeterReaders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.MeterReaders", "NotebookId", "dbo.Notebooks", "Id", cascadeDelete: true);
        }
    }
}
