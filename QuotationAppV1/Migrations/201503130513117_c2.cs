namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quotations", "Author", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotations", "Author", c => c.String());
        }
    }
}
