namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "MyProperty", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "MyProperty");
        }
    }
}
