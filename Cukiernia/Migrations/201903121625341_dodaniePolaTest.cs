namespace Cukiernia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodaniePolaTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkt", "test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produkt", "test");
        }
    }
}
