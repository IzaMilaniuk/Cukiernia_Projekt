using Cukiernia.Models;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.ViewModels
{
    public class PotwierdzenieZamowieniaEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal Wartosc { get; set; }
        public int NumerZamowienia { get; set; }
        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }

    }
}