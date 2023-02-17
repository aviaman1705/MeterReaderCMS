namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerelashionshipbetweentrackandnotebook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "NotebookId", "dbo.Notebooks");
            DropIndex("dbo.Tracks", new[] { "NotebookId" });
            RenameColumn(table: "dbo.Tracks", name: "NotebookId", newName: "Notebook_Id");
            AlterColumn("dbo.Tracks", "Notebook_Id", c => c.Int());
            CreateIndex("dbo.Tracks", "Notebook_Id");
            AddForeignKey("dbo.Tracks", "Notebook_Id", "dbo.Notebooks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Notebook_Id", "dbo.Notebooks");
            DropIndex("dbo.Tracks", new[] { "Notebook_Id" });
            AlterColumn("dbo.Tracks", "Notebook_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tracks", name: "Notebook_Id", newName: "NotebookId");
            CreateIndex("dbo.Tracks", "NotebookId");
            AddForeignKey("dbo.Tracks", "NotebookId", "dbo.Notebooks", "Id", cascadeDelete: true);
        }
    }
}
