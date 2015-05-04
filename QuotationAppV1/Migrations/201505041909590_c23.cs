namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c23 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotations", "v_CategoryRatio");
            DropColumn("dbo.Quotations", "v_super");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotations", "v_super", c => c.String());
            AddColumn("dbo.Quotations", "v_CategoryRatio", c => c.Double(nullable: false));
        }
    }
}
