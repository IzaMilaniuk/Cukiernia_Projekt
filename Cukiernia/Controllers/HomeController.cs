using Cukiernia.DAL;
using Cukiernia.Infrastructure;
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
           
                 
            //pobieranie nowosci ktore : nie sa ukryte w bazie ,po dacie dodania i bierzemy 3 do listy
            ICacheProvider cache = new DefaultCacheProvider();

            List<Kategoria> kategorie;
            if (cache.IsSet(Consts.KategorieCacheKey))
            {
                kategorie = cache.Get(Consts.KategorieCacheKey) as List<Kategoria>; //pobieranie danych z cache
            }
            else
            {
                kategorie = db.Kategorie.ToList(); //pobieranie kategorii
                cache.Set(Consts.KategorieCacheKey, kategorie, 60);
            }



            List<Produkt> nowosci;
             
            //spr czy dane sa w cache
            if(cache.IsSet(Consts.NowosciCacheKey))
            {
                nowosci = cache.Get(Consts.NowosciCacheKey) as List<Produkt>; //pobieranie danych z cache
            }
            else
            {
                nowosci = db.Produkty.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList(); //wczytywanie do cache dancyh z bazy
                cache.Set(Consts.NowosciCacheKey, nowosci, 60);
            }

            List<Produkt> bestseller;

            //spr czy dane sa w cache
            if (cache.IsSet(Consts.BestsellerCacheKey))
            {
                bestseller = cache.Get(Consts.BestsellerCacheKey) as List<Produkt>; //pobieranie danych z cache
            }
            else
            {///Bierzemy ktore nie sa ukryte, flaga ustawiona ze sa bestsellerami, i sortujemy po Guid (spowoduje ze sortujemy po specjalnym identyfikatorze, tworzy dla kazdego kursu 
            //unikalny identyfikator i zostanie to posortowane po innym identyfikatorze i zawsze to bedzie inny . zatem zawsze dostaniemy inny na stronie
                bestseller = db.Produkty.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellerCacheKey, bestseller, 60);
            }
            
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