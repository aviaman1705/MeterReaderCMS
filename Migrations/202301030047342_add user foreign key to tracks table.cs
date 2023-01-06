namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserforeignkeytotrackstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tracks", "UserId");
            AddForeignKey("dbo.Tracks", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "UserId", "dbo.Users");
            DropIndex("dbo.Tracks", new[] { "UserId" });
            DropColumn("dbo.Tracks", "UserId");
        }
    }
}
