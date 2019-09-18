using Cukiernia.Models;

namespace Cukiernia.Infrastructure
{
    public interface IMailService
    {
        void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie);
        void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie);
    }
}