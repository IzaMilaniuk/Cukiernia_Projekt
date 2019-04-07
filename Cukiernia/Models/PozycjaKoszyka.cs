using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.Models
{
    public class PozycjaKoszyka
    {
        public Produkt Produkt { get; set; }
        public int Ilosc { get; set; }
        public decimal Wartosc { get; set; }
    }
}