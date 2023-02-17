namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changenumbertonullableinnotebookstable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notebooks", "Number", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notebooks", "Number", c => c.Int(nullable: false));
        }
    }
}
