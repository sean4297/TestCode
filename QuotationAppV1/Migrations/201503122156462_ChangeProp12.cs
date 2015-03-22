namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProp12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quotations", "Author", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotations", "Author", c => c.String(nullable: false));
        }
    }
}
