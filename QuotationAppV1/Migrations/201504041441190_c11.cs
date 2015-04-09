namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "hide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "hide");
        }
    }
}
