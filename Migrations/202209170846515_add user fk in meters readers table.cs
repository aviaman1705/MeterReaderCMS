namespace MeterReaderCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserfkinmetersreaderstable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
            DropForeignKey("dbo.MeterReaders", "NotebookId", "dbo.Notebooks");
            DropPrimaryKey("dbo.RoleUsers");
            AddColumn("dbo.MeterReaders", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RoleUsers", new[] { "Role_RoleId", "User_UserId" });
            CreateIndex("dbo.MeterReaders", "UserId");
            AddForeignKey("dbo.MeterReaders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeterReaders", "UserId", "dbo.Users");
            DropIndex("dbo.MeterReaders", new[] { "UserId" });
            DropPrimaryKey("dbo.RoleUsers");
            DropColumn("dbo.MeterReaders", "UserId");
            AddPrimaryKey("dbo.RoleUsers", new[] { "User_UserId", "Role_RoleId" });
            AddForeignKey("dbo.MeterReaders", "NotebookId", "dbo.Notebooks", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
        }
    }
}
