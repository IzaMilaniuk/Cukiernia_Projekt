using Cukiernia.Migrations;
using Cukiernia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Cukiernia.DAL
{
    //migrowanie bazy do najnowszej wersji
    public class ProduktyInitializer : MigrateDatabaseToLatestVersion <ProduktyContext, Configuration>
    {
      

        //Dodanie do bazy przykladowe dane
        public static void SeedKProduktyData(ProduktyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId=1, NazwaKategorii="Ciasto", NazwaPlikuIkony="aspnet.png", OpisKategorii="programowanie w asp net" },
                new Kategoria() { KategoriaId=2, NazwaKategorii="Bułka", NazwaPlikuIkony="javascript.png", OpisKategorii="skryptowy język programowania" },
                new Kategoria() { KategoriaId=3, NazwaKategorii="jQuery", NazwaPlikuIkony="jquery.png", OpisKategorii="lekka biblioteka programistyczna dla języka JavaScript" },
                new Kategoria() { KategoriaId=4, NazwaKategorii="Html5", NazwaPlikuIkony="html.png", OpisKategorii="język wykorzystywany do tworzenia i prezentowania stron internetowych www" },
                new Kategoria() { KategoriaId=5, NazwaKategorii="Css3", NazwaPlikuIkony="css.png", OpisKategorii="język służący do opisu formy prezentacji (wyświetlania) stron www" },
                new Kategoria() { KategoriaId=6, NazwaKategorii="Xml", NazwaPlikuIkony="xml.png", OpisKategorii="uniwersalny język znaczników przeznaczony do reprezentowania różnych danych w strukturalizowany sposób" },
                new Kategoria() { KategoriaId=7, NazwaKategorii="C#", NazwaPlikuIkony="csharp.png", OpisKategorii="obiektowy język programowania zaprojektowany dla platformy .Net" }
             };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));  //każda kategoria z listy jest dodawana do bazy
            context.SaveChanges();

            var produkty = new List<Produkt>
            {
                new Produkt() { ProduktId=1, AutorProdukt="Iza", TytulProdukt="Sernik", KategoriaId=1, CenaProdukt= 40, Bestseller=true, NazwaPlikuObrazka="sernik.png",
                DataDodania = DateTime.Now, OpisProdukt="super ciacho"},
                new Produkt() {ProduktId=5, AutorProdukt="Monika", TytulProdukt="Bułka pszenna", KategoriaId=2, CenaProdukt=10, Bestseller=true, NazwaPlikuObrazka="pszenna.png",
                DataDodania=DateTime.Now, OpisProdukt="bulka zwykla"},
                new Produkt() {ProduktId=2, AutorProdukt="Monika", TytulProdukt="Bułka żytnia", KategoriaId=2, CenaProdukt=10, Bestseller=true, NazwaPlikuObrazka="zytnia.png",
                DataDodania=DateTime.Now, OpisProdukt="bulka zytnia"},
                new Produkt() {ProduktId=2, AutorProdukt="Monika", TytulProdukt="Przekładaniec", KategoriaId=1, CenaProdukt=50, Bestseller=true, NazwaPlikuObrazka="przekladaniec.png",
                DataDodania=DateTime.Now, OpisProdukt="Przekładaniec zwykły"},
            };
            produkty.ForEach(k => context.Produkty.AddOrUpdate(k));
            context.SaveChanges();
            
        }
       
    }
}