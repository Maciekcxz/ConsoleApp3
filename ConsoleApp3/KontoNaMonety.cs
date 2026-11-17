
using System;
using System.Collections.Generic;

namespace KontoNaMonety
{
    internal class Konto
    {
        // Aktualna suma pieniędzy wpłaconych na konto.
        private decimal _saldo;

        // Wspólny licznik służący do nadawania kolejnym kontom unikalnych numerów.
        private static int licznikKont = 0;

        // Lista przechowująca kwoty wszystkich poprawnych wpłat.
        private List<decimal> historiaWplat = new List<decimal>();

        // Numer nadany temu konkretnemu kontu podczas jego tworzenia.
        public int NumerKonta { get; }

        public Konto()
        {
            // Zwiększamy licznik i używamy jego aktualnej wartości jako numeru konta.
            licznikKont++;
            NumerKonta = licznikKont;
        }

        public void Wplac(decimal kwota)
        {
            // Konto nie może przyjąć kwoty zerowej ani ujemnej.
            if (kwota <= 0)
            {
                Console.WriteLine("Kwota musi być większa od zera.");
                return;
            }

            // Zwiększamy saldo o wpłaconą kwotę.
            _saldo += kwota;

            // Zapisujemy wpłatę w historii, aby można było ją później wyświetlić.
            historiaWplat.Add(kwota);

            Console.WriteLine(
                $"Wpłacono {kwota} zł. Nowy stan konta: {_saldo} zł."
            );
        }

        public decimal StanKonta()
        {
            // Zwracamy aktualne saldo bez zmieniania jego wartości.
            return _saldo;
        }

        public bool Wyplac(decimal kwota)
        {
            // Wypłacana kwota musi być dodatnia i nie może przekraczać salda.
            if (kwota <= 0 || kwota > _saldo)
            {
                Console.WriteLine("Nie można wypłacić podanej kwoty.");
                return false;
            }

            // Pomniejszamy saldo o wypłaconą kwotę.
            _saldo -= kwota;
            Console.WriteLine($"Wypłacono {kwota} zł. Pozostałe saldo: {_saldo} zł.");
            return true;
        }

        public void PokazHistorieWplat()
        {
            // Wyświetlamy wszystkie kwoty, które zostały poprawnie wpłacone na konto.
            Console.WriteLine("Historia wpłat:");

            // Pokazujemy, ile wpłat znajduje się w historii.
            Console.WriteLine("Liczba wpłat: " + historiaWplat.Count);

            // Przechodzimy po historii i wyświetlamy każdą wpłatę osobno.
            foreach (decimal kwota in historiaWplat)
            {
                Console.WriteLine("Wpłata: " + kwota + " zł");
            }
        }
    }
}
