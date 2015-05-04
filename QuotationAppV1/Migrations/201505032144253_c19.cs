namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c19 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Quotations", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Quotations", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Quotations", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.Quotations", name: "ApplicationUser_Id", newName: "AppUser_Id");
        }
    }
}
