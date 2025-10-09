namespace Harvestly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuItem", "Quantity", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItem", "Quantity", c => c.Double(nullable: false));
        }
    }
}
