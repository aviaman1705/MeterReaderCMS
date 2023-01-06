namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addleftcolumnanddeletetotalcolumnfrommeterreadertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeterReaders", "Left", c => c.Int(nullable: false));
            DropColumn("dbo.MeterReaders", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MeterReaders", "Total", c => c.Int(nullable: false));
            DropColumn("dbo.MeterReaders", "Left");
        }
    }
}
