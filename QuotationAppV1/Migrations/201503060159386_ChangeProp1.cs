namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProp1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quotations", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotations", "DateAdded", c => c.Int(nullable: false));
        }
    }
}
