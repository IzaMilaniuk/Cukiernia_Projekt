using Cukiernia.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Cukiernia.ViewModels;
using Cukiernia.App_Start;
using System.Collections.Generic;
using Cukiernia.DAL;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System;
using Cukiernia.Infrastructure;
using NLog;
using System.Net;
using Hangfire;
using System.Net.Mail;
using Postal;

namespace Cukiernia.Controllers
{
    public class KoszykController : Controller
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private KoszykMenager koszykMenager;
        private ISessionMenager sessionMenager { get; set; }
        private ProduktyContext db ;
     //   private IMailService maileService;

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
        public ActionResult UsunZKoszyka(int produktId)
        {
            int iloscPozycji = koszykMenager.UsunZKoszyka(produktId);
            int iloscPozycjiKoszyka = koszykMenager.PobierzIloscPozycjiKoszyka();
            decimal wartoscKoszyka = koszykMenager.PobierzWartoscKoszyka();

            var wynik = new KoszykUsuwanieViewModel
            {
                IdPozycjiUsuwanej = produktId,
                IloscPozycjiUsuwanej = iloscPozycji,
                KoszykCenaCalkowita = wartoscKoszyka,
                KoszykIloscPozycji = iloscPozycjiKoszyka,
            };
            return Json(wynik);
        }
        public async Task<ActionResult> Zaplac()
        {
            if(Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //Wypełnienie formularza jeśli będą w bazie
                var zamowienie = new Zamowienie
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy = user.DaneUzytkownika.KodPocztowy,
                    Email = user.DaneUzytkownika.Email,
                    Telefon = user.DaneUzytkownika.Telefon
                };
                return View(zamowienie);
        }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Zaplac", "Koszyk") });
        }

        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienie zamowienieSzczegoly)
        {
            if (ModelState.IsValid)
            {
                // pobieramy id uzytkownika aktualnie zalogowanego
                var userId = User.Identity.GetUserId();

                // utworzenie obiektu zamowienia na podstawie tego co mamy w koszyku
                var newOrder = koszykMenager.UtworzZamowienie(zamowienieSzczegoly, userId);

                // szczegóły użytkownika - aktualizacja danych 
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);

                // opróżnimy nasz koszyk zakupów
                koszykMenager.PustyKoszyk();


                string url = Url.Action("WyslaniePotwierdzenieZamowieniaEmail", "Koszyk", new { zamowienieId = newOrder.ZamowienieID, nazwisko = newOrder.Nazwisko }, Request.Url.Scheme);
                BackgroundJob.Enqueue(() => UrlHelpers.CallUrl(url));



                return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
                return View(zamowienieSzczegoly);
        }

        public ActionResult PotwierdzenieZamowienia()
        {
           var name = User.Identity.Name;
           logger.Info("Strona koszyk | potwierdzenie | " + name);
            return View();
        }

        public ActionResult WyslaniePotwierdzenieZamowieniaEmail(int zamowienieId, string nazwisko)
        {

            var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Produkt")
                                              .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);
            if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            dynamic email = new Email("PotwierdzenieZamowienia");
            //   var email = new PotwierdzenieZamowieniaEmail();
            //    PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();

            //    dynamic email = new Postal.Email("PotwierdzenieZamowienia");

            email.To = zamowienie.Email;
            email.From = ("izamilaniuk@gmail.com");
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
            email.Subject = "Welcome";
            email.IsBodyHtml = true;

            //   email.Subject = "Potwierdzenie Zamównienia";
            //  var email = new PotwierdzenieZamowieniaEmail();
            //  return new EmailViewResult(email);


            email.Send();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
            //  return new HttpStatusCodeResult(HttpStatusCode.OK);
            //maileService.WyslaniePotwierdzenieZamowieniaEmail(newOrder);
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}