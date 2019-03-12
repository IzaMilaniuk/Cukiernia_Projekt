namespace Cukiernia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodaniePolaOpisSkrocony : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkt", "Ukryty", c => c.Boolean(nullable: false));
            AddColumn("dbo.Produkt", "OpisSkrocony", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produkt", "OpisSkrocony");
            DropColumn("dbo.Produkt", "Ukryty");
        }
    }
}
