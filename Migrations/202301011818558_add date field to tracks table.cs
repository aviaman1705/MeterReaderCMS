namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatefieldtotrackstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Date");
        }
    }
}
