using Cukiernia.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Cukiernia.DAL
{
    public class ProduktyContext: IdentityDbContext<ApplicationUser>
    {
        public ProduktyContext() : base("ProduktyContext")
        {

        }
        static ProduktyContext()
        {   //wywołanie ProduktyInitializer , by dane z niego pojawiły sie w bazie
            Database.SetInitializer < ProduktyContext>(new ProduktyInitializer());

        }
        public static ProduktyContext Create()
        {
            return new ProduktyContext();
        }

        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }

        //Kiedy będzie tworzona baza nie bęzie dodawane s na końcu
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            //using System.Data.Enity.ModelConfiguration.Conventions;;
            //Wyłącza konwersje , która automatycznie tworzy liczbe mnogą dla nazw tabelw bazie danych
            //Zamiast Kategorie zostałaby stworzona tabela o nazwie Kategoria
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}