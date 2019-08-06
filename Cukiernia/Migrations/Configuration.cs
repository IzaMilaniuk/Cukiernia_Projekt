namespace Cukiernia.Migrations
{
    using DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Cukiernia.DAL.ProduktyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cukiernia.DAL.ProduktyContext";
        }

        //metoda seed,ktora wpisuje przykladowe dane
        protected override void Seed(Cukiernia.DAL.ProduktyContext context)
        {
          ProduktyInitializer.SeedKProduktyData(context);
            ProduktyInitializer.SeedUzytkownicy(context);
         //kom by nie nadpisywac rekordow w bazie



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
