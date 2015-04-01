namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "CategoryName");
        }
    }
}
