namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetackstablefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "ElectricityMeterCalled", c => c.Int(nullable: false));
            AddColumn("dbo.Tracks", "ElectricityMeterUnCalled", c => c.Int(nullable: false));
            DropColumn("dbo.Tracks", "Number");
            DropColumn("dbo.Tracks", "StreetName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "StreetName", c => c.String());
            AddColumn("dbo.Tracks", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.Tracks", "ElectricityMeterUnCalled");
            DropColumn("dbo.Tracks", "ElectricityMeterCalled");
        }
    }
}
