namespace QuotationAppV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c25 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotations", "v_Quote");
            DropColumn("dbo.Quotations", "v_Author");
            DropColumn("dbo.Quotations", "v_Category");
            DropColumn("dbo.Quotations", "v_CategoryCount");
            DropColumn("dbo.Quotations", "v_AuthorCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotations", "v_AuthorCount", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "v_CategoryCount", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "v_Category", c => c.String());
            AddColumn("dbo.Quotations", "v_Author", c => c.String());
            AddColumn("dbo.Quotations", "v_Quote", c => c.String());
        }
    }
}
