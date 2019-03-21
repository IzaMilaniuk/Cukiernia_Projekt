using Cukiernia.DAL;
using Cukiernia.Models;
using Cukiernia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cukiernia.Controllers
{
    public class HomeController : Controller
    {
        private ProduktyContext db = new ProduktyContext();
        public ActionResult Index()
        {
            var kategorie = db.Kategorie.ToList(); //pobieranie kategorii
            //pobieranie nowosci ktore : nie sa ukryte w bazie ,po dacie dodania i bierzemy 3 do listy
            var nowosci = db.Produkty.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
            //Bierzemy ktore nie sa ukryte, flaga ustawiona ze sa bestsellerami, i sortujemy po Guid (spowoduje ze sortujemy po specjalnym identyfikatorze, tworzy dla kazdego kursu 
            //unikalny identyfikator i zostanie to posortowane po innym identyfikatorze i zawsze to bedzie inny . zatem zawsze dostaniemy inny na stronie
            var bestseller = db.Produkty.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
            //zainstancjonujemy nasz model
            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Bestsellery = bestseller
            };
            //przekazujemy nasz model do widoku
            return View(vm);
        }

        //metoda ktora zwraca strony statyczne
        //metoda strony statycznej,ktora bedzie odpowiedzialna za wyswietlanie widokow stron statycznych
        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }
    }
}