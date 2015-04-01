namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotations", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotations", "CategoryName", c => c.String());
        }
    }
}
