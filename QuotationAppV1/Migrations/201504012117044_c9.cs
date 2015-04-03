namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Quotations", "ApplicationUser_Id");
            AddForeignKey("dbo.Quotations", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Quotations", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Quotations", "ApplicationUser_Id");
        }
    }
}
