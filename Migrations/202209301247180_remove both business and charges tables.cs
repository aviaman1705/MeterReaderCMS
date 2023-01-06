namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removebothbusinessandchargestables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Charges", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Charges", new[] { "BusinessId" });
            DropTable("dbo.Businesses");
            DropTable("dbo.Charges");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Charges", "BusinessId");
            AddForeignKey("dbo.Charges", "BusinessId", "dbo.Businesses", "Id", cascadeDelete: true);
        }
    }
}
