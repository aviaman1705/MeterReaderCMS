namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notebooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrackNotebooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackID = c.Int(nullable: false),
                        NotebbookID = c.Int(nullable: false),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notebooks", t => t.NotebbookID, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.NotebbookID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElectricityMeterCalled = c.Int(nullable: false),
                        ElectricityMeterUnCalled = c.Int(nullable: false),
                        Desc = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Streets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.TrackNotebooks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.TrackNotebooks", "NotebbookID", "dbo.Notebooks");
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.Tracks", new[] { "UserId" });
            DropIndex("dbo.TrackNotebooks", new[] { "NotebbookID" });
            DropIndex("dbo.TrackNotebooks", new[] { "TrackID" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Streets");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Tracks");
            DropTable("dbo.TrackNotebooks");
            DropTable("dbo.Notebooks");
        }
    }
}
