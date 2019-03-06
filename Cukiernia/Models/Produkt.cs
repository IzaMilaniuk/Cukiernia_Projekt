using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public int KategoriaId { get; set; }
        public string TytulProdukt { get; set; }
        public string AutorProdukt { get; set; }
        public DateTime DataDodania { get; set; }
        public string NazwaPlikuObrazka { get; set; }
        public string OpisProdukt { get; set; }
        public string CenaProdukt { get; set; }
        public bool Bestseller { get; set; }
        public virtual Kategoria Kategoria { get; set; }

    }
}