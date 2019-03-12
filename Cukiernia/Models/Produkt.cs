using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cukiernia.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public int KategoriaId { get; set; }
        [Required(ErrorMessage ="Wprowadź nazwę Produktu")]
        [StringLength(100)]

        public string TytulProdukt { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę Autora")]
        [StringLength(100)]
        public string AutorProdukt { get; set; }
        public DateTime DataDodania { get; set; }
        [StringLength(100)]
        public string NazwaPlikuObrazka { get; set; }
        public string OpisProdukt { get; set; }
        public decimal CenaProdukt { get; set; }
        public bool Bestseller { get; set; }
        public virtual Kategoria Kategoria { get; set; }

    }
}