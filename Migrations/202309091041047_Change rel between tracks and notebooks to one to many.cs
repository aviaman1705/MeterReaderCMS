namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changerelbetweentracksandnotebookstoonetomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrackNotebooks", "NotebbookID", "dbo.Notebooks");
            DropForeignKey("dbo.TrackNotebooks", "TrackID", "dbo.Tracks");
            DropIndex("dbo.TrackNotebooks", new[] { "TrackID" });
            DropIndex("dbo.TrackNotebooks", new[] { "NotebbookID" });
            AddColumn("dbo.Tracks", "NotebookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tracks", "NotebookId");
            AddForeignKey("dbo.Tracks", "NotebookId", "dbo.Notebooks", "Id", cascadeDelete: true);
            DropTable("dbo.TrackNotebooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrackNotebooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackID = c.Int(nullable: false),
                        NotebbookID = c.Int(nullable: false),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Tracks", "NotebookId", "dbo.Notebooks");
            DropIndex("dbo.Tracks", new[] { "NotebookId" });
            DropColumn("dbo.Tracks", "NotebookId");
            CreateIndex("dbo.TrackNotebooks", "NotebbookID");
            CreateIndex("dbo.TrackNotebooks", "TrackID");
            AddForeignKey("dbo.TrackNotebooks", "TrackID", "dbo.Tracks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackNotebooks", "NotebbookID", "dbo.Notebooks", "Id", cascadeDelete: true);
        }
    }
}
