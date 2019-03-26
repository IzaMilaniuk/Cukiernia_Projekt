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
        public ActionResult Lista(string nazwaKategori)
        {
            var kategoria = db.Kategorie.Include("Produkt").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategori.ToUpper()).Single(); //pobieramy kategoie i przekazujemy też kursy

            var produkty = kategoria.Produkt.ToList();
            return View(produkty);
        }
        public ActionResult Szczegoly(string id)
        {
            return View();
        }


        [ChildActionOnly]  //Ta akcja moze byc wywolana tylko z poziomu innej akcji
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu", kategorie);  //przekazujemy kategorie do widoku 
        }
    }
}