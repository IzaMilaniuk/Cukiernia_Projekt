using Cukiernia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cukiernia.DAL
{
    public class ProduktyInitializer : DropCreateDatabaseAlways <ProduktyContext>
    {
        protected override void Seed(ProduktyContext context)
        {
            SeedKProduktyData(context);
            base.Seed(context);
        }

        //Dodanie do bazy przykladowe dane
        private void SeedKProduktyData(ProduktyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() {KategoriaId=1, NazwaKategorii = "Ciasta", NazwaPlikuIkony ="ciasta.png",OpisKategorii="opis ciasta" },
                new Kategoria() {KategoriaId=2, NazwaKategorii = "Bułki", NazwaPlikuIkony ="Bułki.png",OpisKategorii="opis Bułki" },
                new Kategoria() {KategoriaId=3, NazwaKategorii = "Bułki", NazwaPlikuIkony ="Bułki.png",OpisKategorii="opis Bułki" },
                new Kategoria() {KategoriaId=4, NazwaKategorii = "Ciasta_fit", NazwaPlikuIkony ="Ciasta_fit.png",OpisKategorii="opis Ciasta_fit" },
                new Kategoria() {KategoriaId=5, NazwaKategorii = "Chleb", NazwaPlikuIkony ="Chleb.png",OpisKategorii="opis Chleb" },
                new Kategoria() {KategoriaId=6, NazwaKategorii = "xml", NazwaPlikuIkony ="xml.png",OpisKategorii="opis xml" },
                new Kategoria() {KategoriaId=7, NazwaKategorii = "css", NazwaPlikuIkony ="css.png",OpisKategorii="opis css" },
             };
            kategorie.ForEach(k => context.Kategorie.Add(k));
            context.SaveChanges();

            var produkty = new List<Produkt>
            {
                new Produkt() { ProduktId=1, AutorProdukt="Iza", TytulProdukt="Sernik", KategoriaId=1, CenaProdukt= 40, Bestseller=true, NazwaPlikuObrazka="sernik.png",
                DataDodania = DateTime.Now, OpisProdukt="super ciacho"},
                new Produkt() {ProduktId=2, AutorProdukt="Monika", TytulProdukt="Bułka pszenna", KategoriaId=2, CenaProdukt=10, Bestseller=true, NazwaPlikuObrazka="bulki.png",
                DataDodania=DateTime.Now, OpisProdukt="bulka zwykla"},
                new Produkt() {ProduktId=2, AutorProdukt="Monika", TytulProdukt="Bułka żytnia", KategoriaId=2, CenaProdukt=10, Bestseller=true, NazwaPlikuObrazka="bulki.png",
                DataDodania=DateTime.Now, OpisProdukt="bulka zytnia"},
                new Produkt() {ProduktId=2, AutorProdukt="Monika", TytulProdukt="Przekładaniec", KategoriaId=1, CenaProdukt=50, Bestseller=true, NazwaPlikuObrazka="ciasto.png",
                DataDodania=DateTime.Now, OpisProdukt="Przekładaniec zwykły"},
            };
            produkty.ForEach(k => context.Produkty.Add(k));
            context.SaveChanges();
            
        }
       
    }
}