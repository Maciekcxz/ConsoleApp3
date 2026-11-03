using System;

namespace KontoNaMonety
{
    internal class Konto
    {
        private decimal _saldo;
        private static int licznikKont = 0;

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
                Console.WriteLine("Kwota musi być większa od zera.");
                return;
            }

            _saldo += kwota;

            Console.WriteLine($"Wpłacono {kwota} zł. Nowy stan konta: {_saldo} zł.");
        }

        public decimal StanKonta()
        {
            return _saldo;
        }
    }
}