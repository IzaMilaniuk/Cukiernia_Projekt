

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


namespace Cukiernia.Controllers
{
    [Authorize]

    public class ManageController : Controller
    {
        private ProduktyContext db = new ProduktyContext();

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
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
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;


            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;

            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                DaneUzytkownika = user.DaneUzytkownika
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]DaneUzytkownika daneUzytkownika)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.DaneUzytkownika = daneUzytkownika;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }
        public ActionResult ListaZamowien()
        {
            var name = User.Identity.Name;
            //   logger.Info("Admin zamowienia | " + name);

            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Zamowienie> zamowieniaUzytkownika;

            // Dla administratora zwracamy wszystkie zamowienia
            if (isAdmin)
            {
                zamowieniaUzytkownika = db.Zamowienia.Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();
            }
            else
            {
                //zamowienie dla danego usera
                var userId = User.Identity.GetUserId();
                zamowieniaUzytkownika = db.Zamowienia.Where(o => o.UserId == userId).Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();
            }
            //wrot listy zamowien do widoku
            return View(zamowieniaUzytkownika);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public StanZamowienia ZmianaStanuZamowienia(Zamowienie zamowienie)
        {
            Zamowienie zamowienieDoModyfikacji = db.Zamowienia.Find(zamowienie.ZamowienieID);
            zamowienieDoModyfikacji.StanZamowienia = zamowienie.StanZamowienia;
            db.SaveChanges();

            if (zamowienieDoModyfikacji.StanZamowienia == StanZamowienia.Zrealizowane)
            {
                //      this.mailService.WyslanieZamowienieZrealizowaneEmail(zamowienieDoModyfikacji);
            }

            return zamowienie.StanZamowienia;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DodajProdukt(int? produktId, bool? potwierdzenie)
        {
            Produkt produkt;
            if (produktId.HasValue)
            {
                ViewBag.EditMode = true;
                produkt = db.Produkty.Find(produktId);
            }
            else
            {
                ViewBag.EditMode = false;
                produkt = new Produkt();
            }

            var result = new EditProduktViewModel();
            result.Kategorie = db.Kategorie.ToList();
            result.Produkt = produkt;
            result.Potwierdzenie = potwierdzenie;

            return View(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DodajProdukt(EditProduktViewModel model, HttpPostedFileBase file)
        {
            if (model.Produkt.ProduktId > 0)
            {
                // modyfikacja kursu
                db.Entry(model.Produkt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid) //czy wypełniony prawidłowo wypełniony
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.ObrazkiFolderWzgledny), filename);
                        //   ia = DateTime.Now;

                        file.SaveAs(path);

                        model.Produkt.NazwaPlikuObrazka = filename;
                        model.Produkt.DataDodania = DateTime.Now;

                        db.Entry(model.Produkt).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
                    }
                    else
                    {
                        var kategorie = db.Kategorie.ToList();

                        model.Kategorie = kategorie;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var kategorie = db.Kategorie.ToList();
                    model.Kategorie = kategorie;
                    return View(model);
                }

            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult UkryjProdukt(int produktId)
        {
            var produkt = db.Produkty.Find(produktId);
            produkt.Ukryty = true;
            db.SaveChanges();

            return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult PokazProdukt(int produktId)
        {
            var produkt = db.Produkty.Find(produktId);
        produkt.Ukryty = false;
            db.SaveChanges();

            return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
        }
       
    }
}