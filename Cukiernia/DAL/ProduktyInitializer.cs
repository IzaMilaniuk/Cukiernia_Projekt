using Cukiernia.Migrations;
using Cukiernia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                new Kategoria() { KategoriaId=1, NazwaKategorii="Pieczywo", NazwaPlikuIkony="aspnet.png", OpisKategorii="programowanie w asp net" },
                new Kategoria() { KategoriaId=2, NazwaKategorii="Ciasta", NazwaPlikuIkony="javascript.png", OpisKategorii="skryptowy język programowania" },
                new Kategoria() { KategoriaId=3, NazwaKategorii="Fit Ciasta", NazwaPlikuIkony="jquery.png", OpisKategorii="lekka biblioteka programistyczna dla języka JavaScript" },
                new Kategoria() { KategoriaId=4, NazwaKategorii="Ciasteczka", NazwaPlikuIkony="html.png", OpisKategorii="język wykorzystywany do tworzenia i prezentowania stron internetowych www" },
                new Kategoria() { KategoriaId=5, NazwaKategorii="Nowości", NazwaPlikuIkony="css.png", OpisKategorii="język służący do opisu formy prezentacji (wyświetlania) stron www" },
                new Kategoria() { KategoriaId=6, NazwaKategorii="Babeczni", NazwaPlikuIkony="xml.png", OpisKategorii="uniwersalny język znaczników przeznaczony do reprezentowania różnych danych w strukturalizowany sposób" },
       
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
        public static void SeedUzytkownicy(ProduktyContext db)
        {
            //Pobieramy identyfikator urzytkownika
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@cukiernia.pl";
            const string password = "P@ssw0rd";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)    //Sprawdzamy czy mamy urzytkownika o nazwie admin@cukiernia.pl jesli nie , tworzymy go
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var result = userManager.Create(user, password);
            }

            // utworzenie roli Admin jeśli nie istnieje 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // dodanie uzytkownika do roli Admin jesli juz nie jest w roli
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}