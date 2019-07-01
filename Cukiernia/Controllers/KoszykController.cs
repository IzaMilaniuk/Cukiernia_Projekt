using Cukiernia.DAL;
using Cukiernia.Infrastructure;
using Cukiernia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cukiernia.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykMenager koszykMenager;
        private ISessionMenager sessionMenager { get; set; }
        private ProduktyContext db ;

        public KoszykController()
        {
            db = new ProduktyContext();
            sessionMenager = new SessionMenager();
            koszykMenager = new KoszykMenager(sessionMenager,db);
        }
        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykMenager.PobierzKoszyk();
            var cenaCalowita = koszykMenager.PobierzWartoscKoszyka();
            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka =pozycjeKoszyka,
                CenaCalkowita =cenaCalowita
            };
            return View(koszykVM);
        }
        public ActionResult DodajDoKoszyka(int id)
        {
            koszykMenager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }
        public int PobierzIloscElementowKoszyka()
        {
           return koszykMenager.PobierzIloscPozycjiKoszyka();

        }
    }
}