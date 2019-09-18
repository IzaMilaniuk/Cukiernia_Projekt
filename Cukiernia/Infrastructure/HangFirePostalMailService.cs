using Hangfire;
using Cukiernia.Models;
using System.Web;
using System.Web.Mvc;

namespace Cukiernia.Infrastructure
{
    public class HangFirePostalMailService : IMailService
    {
        public void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("WyslaniePotwierdzenieZamowieniaEmail", "Manage", new { zamowienieId = zamowienie.ZamowienieID, nazwisko = zamowienie.Nazwisko }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => UrlHelpers.CallUrl(url));
        }

        public void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("WyslanieZamowienieZrealizowaneEmail", "Manage", new { zamowienieId = zamowienie.ZamowienieID, nazwisko = zamowienie.Nazwisko }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => UrlHelpers.CallUrl(url));
        }
    }
}