namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "userEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "userEmail");
        }
    }
}
