using Cukiernia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cukiernia.Controllers
{
    public class ProduktyController : Controller
    {
        private ProduktyContext db = new ProduktyContext();
        // GET: Produkty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategori, string searchQuery = null)
        {
            var kategoria = db.Kategorie.Include("Produkt").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategori.ToUpper()).Single(); //pobieramy kategoie i przekazujemy też produkty

            var produkty = kategoria.Produkt.Where(a => (searchQuery == null) ||
                                                   a.TytulProdukt.ToLower().Contains(searchQuery.ToLower()) ||
                                                   a.AutorProdukt.ToLower().Contains(searchQuery.ToLower()) &&
                                                   !a.Ukryty);
            if(Request.IsAjaxRequest())
            {
                return PartialView("_ProduktyList", produkty);
            }
            return View(produkty);
        }
        public ActionResult Szczegoly(int id)
        {
            var produkt = db.Produkty.Find(id);

            return View(produkt);
        }


        [ChildActionOnly]  //Ta akcja moze byc wywolana tylko z poziomu innej akcji
       [OutputCache (Duration =60000)] //zapis dancyh z bazy do cache na 1 dzien
        public ActionResult KategorieMenu()
        {
            
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu", kategorie);  //przekazujemy kategorie do widoku 
        }

        public ActionResult ProduktyPodpowiedzi(string term)
        {//pobiera produkty ograniczamy , nie moga byc ukryte.duze litery
            //pobieranie 5 produktow(podpowiedzi) i zwraca tylko label ,czyli info o producie
            //wykorzystujemy select by wziasc tytul produktu
            var produkty =this.db.Produkty.Where(a => !a.Ukryty && a.TytulProdukt.ToLower().Contains(term.ToLower()))
                .Take(5).Select(a => new { label = a.TytulProdukt });

            //zwracamy jako json
            return Json(produkty, JsonRequestBehavior.AllowGet);
        }
    }
}