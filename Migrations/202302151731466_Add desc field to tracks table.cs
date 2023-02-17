namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddescfieldtotrackstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Desc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Desc");
        }
    }
}
