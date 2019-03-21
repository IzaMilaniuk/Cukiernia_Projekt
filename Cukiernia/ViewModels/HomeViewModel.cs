using Cukiernia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.ViewModels
{
    public class HomeViewModel
    {
        //chcemy by zawieral :liste kategorii, bestselerow,nowosci
        //wykorzystujemy IEnumerable,poniewaz jest to generyczny interfejs,ktory implementuje wszystkie kolekcje
        //czyli bedziemy mogli zwrocic dowolna kolekcje. jest to najlepsza tchnika

        public IEnumerable<Kategoria> Kategorie { get; set; }  //pierwsza lista
        public IEnumerable<Produkt> Nowosci { get; set; }
        public IEnumerable<Produkt> Bestsellery { get; set; }


    }
}