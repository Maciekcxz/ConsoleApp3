
using System;
using System.Collections.Generic;

namespace KontoNaMonety
{
    internal class Konto
    {
        private decimal _saldo;

        private static int licznikKont = 0;

        private List<decimal> historiaWplat = new List<decimal>();

        public int NumerKonta { get; }

        public Konto()
        {
            licznikKont++;
            NumerKonta = licznikKont;
        }

        public void Wplac(decimal kwota)
        {
            if (kwota <= 0)
            {
                return;
            }

            _saldo += kwota;

            historiaWplat.Add(kwota);
        }

        public decimal StanKonta()
        {
            return _saldo;
        }

        public void PokazHistorieWplat()
        {
            Console.WriteLine("\nHistoria wpłat:");
            Console.WriteLine("Liczba wpłat: " + historiaWplat.Count);

            foreach (decimal kwota in historiaWplat)
            {
                Console.WriteLine("Wpłata: " + kwota + " zł");
            }
        }
    }
}
