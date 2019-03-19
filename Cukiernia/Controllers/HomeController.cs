using Cukiernia.DAL;
using Cukiernia.Models;
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
            //pobieranie liste kategorii z bazy
            var listaKategorii = db.Kategorie.ToList();

          
            return View();
        }

        //metoda ktora zwraca strony statyczne
        //metoda strony statycznej,ktora bedzie odpowiedzialna za wyswietlanie widokow stron statycznych
        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }
    }
}