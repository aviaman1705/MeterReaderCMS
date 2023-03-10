namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCalledandUnCalledfieldstoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeterReaders", "Called", c => c.Int(nullable: false));
            AlterColumn("dbo.MeterReaders", "UnCalled", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeterReaders", "UnCalled", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.MeterReaders", "Called", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
