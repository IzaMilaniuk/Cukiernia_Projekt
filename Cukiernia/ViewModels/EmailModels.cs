using Postal;
using Cukiernia.Models;
using System.Collections.Generic;


namespace Cukiernia.ViewModels
{
    public class WysylaniePotwierdzenieZamowieniaEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal Wartosc { get; set; }
        public int NumerZamowienia { get; set; }
    //    public string sciezkaObrazka { get; set; }
        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }

    public class WysylanieZamowienieZrealizowaneEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public int NumerZamowienia { get; set; }
    }
}
