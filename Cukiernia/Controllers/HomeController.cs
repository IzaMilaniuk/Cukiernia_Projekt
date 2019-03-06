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
            
            Kategoria kategoria = new Kategoria { NazwaKategorii = "asp.net_mvc", NazwaPlikuIkony = "aspNetMvc.png ", OpisKategorii = "opis" };
            db.Kategorie.Add(kategoria);
            db.SaveChanges(); 
            return View();
        }
    }
}