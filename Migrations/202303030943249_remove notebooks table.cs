namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removenotebookstable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
            DropForeignKey("dbo.Tracks", "Notebook_Id", "dbo.Notebooks");
            DropIndex("dbo.Tracks", new[] { "Notebook_Id" });
            DropPrimaryKey("dbo.UserRoles");
            AddPrimaryKey("dbo.UserRoles", new[] { "User_UserId", "Role_RoleId" });
            DropColumn("dbo.Tracks", "Notebook_Id");
            DropTable("dbo.Notebooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notebooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(),
                        StreetName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tracks", "Notebook_Id", c => c.Int());
            DropPrimaryKey("dbo.UserRoles");
            AddPrimaryKey("dbo.UserRoles", new[] { "Role_RoleId", "User_UserId" });
            CreateIndex("dbo.Tracks", "Notebook_Id");
            AddForeignKey("dbo.Tracks", "Notebook_Id", "dbo.Notebooks", "Id");
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
        }
    }
}
