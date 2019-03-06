namespace Cukiernia.Models
{
    public class PozycjaZamowienia
    {
        public int PozycjaZamowieniaId { get; set; }
        public int ZamowienieID { get; set; }
        public int ProduktId { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaZakupu { get; set; }

        public virtual Produkt produkt { get; set; }
        public virtual Zamowienie zamowienie { get; set; }

    }
}