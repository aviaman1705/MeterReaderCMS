namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstreetnametonotebookstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notebooks", "StreetName", c => c.String(defaultValue:"אין כתובת"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notebooks", "StreetName");
        }
    }
}
