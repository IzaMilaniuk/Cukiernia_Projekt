using Cukiernia.DAL;
using Cukiernia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.Infrastructure
{
    public class KoszykMenager
    {
        private ProduktyContext db;
        private ISessionMenager session;


        public KoszykMenager(ISessionMenager session, ProduktyContext db)
        {
            this.session = session;
            this.db = db;
        }
        public List<PozycjaKoszyka> PobierzKoszyk()
        {
            List<PozycjaKoszyka> koszyk;
            if (session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz) == null) //czy pozycja koszyka zapisana jest w sesji
            { //jesli null to nic nie jest zapisane w sesji
                koszyk = new List<PozycjaKoszyka>();

            }
            else
            {
                koszyk = session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz) as List<PozycjaKoszyka>;
            }
            return koszyk;
        }
        public void DodajDoKoszyka(int produktId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.Produkt.ProduktId == produktId); //sprawdzenie czy w koszyku jest wybrany produkt

            if (pozycjaKoszyka != null)
                pozycjaKoszyka.Ilosc++;
            else
            {
                var produktDoDodania = db.Produkty.Where(k => k.ProduktId == produktId).SingleOrDefault();
                if(produktDoDodania != null)
                {
                    var nowaPozycjaKoszyka = new PozycjaKoszyka()
                    {
                        Produkt = produktDoDodania,
                        Ilosc = 1,
                        Wartosc = produktDoDodania.CenaProdukt
                    };
                    koszyk.Add(nowaPozycjaKoszyka);

                }
            }

            session.Set(Consts.KoszykSessionKlucz, koszyk);

        }
        public int UsunZKoszyka(int produktId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.Produkt.ProduktId == produktId);

            if(pozycjaKoszyka != null)  
            {
                if(pozycjaKoszyka.Ilosc >1)
                {
                    pozycjaKoszyka.Ilosc--;
                    return pozycjaKoszyka.Ilosc;
                }
                else
                {
                    koszyk.Remove(pozycjaKoszyka);
                }
            }
            return 0;
        }
        public decimal  PobierzWartoscKoszyka()    //decimal to wartosc
        {
            var koszyk = PobierzKoszyk();
            return koszyk.Sum(k => (k.Ilosc * k.Produkt.CenaProdukt));

        }
        public int PobierzIloscPozycjiKoszyka()
        {
            var koszyk = PobierzKoszyk();
            int ilosc = koszyk.Sum(k => k.Ilosc);
            return ilosc;
        }
        public Zamowienie UtworzZamowienie(Zamowienie noweZamowienie, string userId)
        {
            var koszyk = PobierzKoszyk();
            noweZamowienie.DataDodania = DateTime.Now;
            noweZamowienie.UserId = userId;
            db.Zamowienia.Add(noweZamowienie);

            if (noweZamowienie.PozycjeZamowienia == null)
                noweZamowienie.PozycjeZamowienia = new List<PozycjaZamowienia>();
            decimal koszykWartosc = 0;
            foreach (var koszykElement in koszyk)
            {
                var nowaPozycjaZamowienia = new PozycjaZamowienia()
                {
                    ProduktId = koszykElement.Produkt.ProduktId,
                    Ilosc = koszykElement.Ilosc,
                    CenaZakupu = koszykElement.Produkt.CenaProdukt
                };
                koszykWartosc += (koszykElement.Ilosc * koszykElement.Produkt.CenaProdukt);
                noweZamowienie.PozycjeZamowienia.Add(nowaPozycjaZamowienia);
            }
            noweZamowienie.WartoscZamowienia = koszykWartosc;
            db.SaveChanges();

            return noweZamowienie;
            }


        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz,null);
        }
      }
    }
    
