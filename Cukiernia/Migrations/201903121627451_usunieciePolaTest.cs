namespace Cukiernia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usunieciePolaTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Produkt", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produkt", "test", c => c.String());
        }
    }
}
