namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createChargesandBusinessestables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BusinessId = c.Int(nullable: false),
                        PayUntil = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .Index(t => t.BusinessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charges", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Charges", new[] { "BusinessId" });
            DropTable("dbo.Charges");
            DropTable("dbo.Businesses");
        }
    }
}
