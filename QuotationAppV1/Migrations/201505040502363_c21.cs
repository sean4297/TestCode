namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "v_super", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "v_super");
        }
    }
}
