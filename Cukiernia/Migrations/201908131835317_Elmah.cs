namespace Cukiernia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Elmah : DbMigration
    {
        public override void Up()
        {
            SqlFile("../Migrations/ELMAH_SQLServerSetup.sql");
        }
        
        public override void Down()
        {
        }
    }
}
