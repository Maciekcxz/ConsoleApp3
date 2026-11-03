using System;
using System.Globalization;
using KontoNaMonety;

namespace ConsoleApp3
{
    internal class Automat
    {
        private readonly decimal[] PrzyjmowaneMonety = { 0.5m, 1m, 2m, 5m, 0.20m, 0.10m };
        private int _iloscMonet;

        public void Wrzucmonete(string wartoscMonety, Konto konto)
        {
            bool czyPoprawna = decimal.TryParse(wartoscMonety, CultureInfo.InvariantCulture, out decimal wartosc);

            if (!czyPoprawna)
            {
                Console.WriteLine("Nieprawidłowa wartość monety.");
                return;
            }

            foreach (decimal nominal in PrzyjmowaneMonety)
            {
                if (wartosc == nominal)
                {
                    konto.Wplac(nominal);
                    _iloscMonet++;
                    return;
                }
            }

            Console.WriteLine("Nieprawidłowa moneta.");
        }
    }
}