using Cukiernia.Models;
using Cukiernia.ViewModels;
using Postal;

namespace Cukiernia.Infrastructure
{
    public class PostalMailService 
    {
        public void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie)
        {
        //  WysylaniePotwierdzenieZamowieniaEmail email = new WysylaniePotwierdzenieZamowieniaEmail();
            dynamic email = new Email("PotwierdzenieZamowienia");
            email.To = zamowienie.Email;
            email.From = "mariuszjurczenko@gmail.com";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
          //  email.sciezkaObrazka = AppConfig.ObrazkiFolderWzgledny;
            email.Send();
        }

        public void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie)
        {
            dynamic email = new Email("ZamowienieZrealizowane");
            email.To = zamowienie.Email;
            email.Subject = "Zamowienie-Zrealizowane";
            email.From = "izamilaniuk@gmail.com";
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.Send();
        }
    }
}